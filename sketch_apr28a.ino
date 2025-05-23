// Параметры шагового двигателя
const int stepPin = 3;      // STEP драйвера
const int dirPin = 4;       // DIR драйвера
const int stepsPerRev = 3200; // Шагов на 1 оборот (для NEMA 17 = 3200)
int stepsPerLayer = 100;     // Шагов для укладки 1 витка (подбирается экспериментально!)
int stepFree = 100;         //шаг для свободного вращения
// Датчик Холла
const int hallSensorPin = 2;
volatile bool rotationDetected = false;
unsigned int rotations = 0;

String serialData; // Переменная для хранения команды

void setup() {
  // put your setup code here, to run once:
 Serial.begin(9600);
 
  // Настройка шагового двигателя
  pinMode(stepPin, OUTPUT);
  pinMode(dirPin, OUTPUT);
  digitalWrite(dirPin, HIGH);  // Направление вращения (можно менять)

 // Настройка датчика Холла
  pinMode(hallSensorPin, INPUT_PULLUP);
  attachInterrupt(digitalPinToInterrupt(hallSensorPin), hallInterrupt, FALLING);

  
}

void loop() {

  if(Serial.available() > 0) {
    serialData = Serial.readStringUntil('\n'); // Читаем строку до символа новой строки

      if (serialData.startsWith("F")) { // Вперёд
        digitalWrite(dirPin, HIGH); 
      }

      else if (serialData.startsWith("B")) { // Вперёд
       digitalWrite(dirPin, LOW); 
      }

      else if (serialData.startsWith("STEPS:")){
       int stepsToMove = serialData.substring(6).toInt(); // Извлекаем число после "STEPS:"
        stepsPerLayer = stepsToMove;
      }

       else if (serialData.startsWith("STEPS_FREE:")){
       int stepsToMove = serialData.substring(11).toInt(); // Извлекаем число после "STEPS:"
        stepFree = stepsToMove;
      }

       else if (serialData.startsWith("MUVE_FREE")){
       rotateFreeMotor();
      }
   }


  if (rotationDetected) {
    rotateStepper();       // Поворот на заданный угол
    rotationDetected = false;
    rotations++;
    Serial.print("Sdelano oborotov: ");
    Serial.println(rotations);
  }
}

// Прерывание по датчику Холла
void hallInterrupt() {
  static unsigned long lastInterruptTime = 0;
  if (millis() - lastInterruptTime > 500) {  // Защита от дребезга (100 мс)
    rotationDetected = true;
    lastInterruptTime = millis();
  }
}

//Свободный повороот шагового двигателя
void rotateFreeMotor(){
   for (int i = 0; i < stepFree; i++) {
    digitalWrite(stepPin, HIGH); 
    delayMicroseconds(700);  // Скорость шага (можно уменьшить для ускорения)
    digitalWrite(stepPin, LOW);
    delayMicroseconds(500);
  }
}

// Поворот шагового двигателя
void rotateStepper() {
  for (int i = 0; i < stepsPerLayer; i++) {
    digitalWrite(stepPin, HIGH); 
    delayMicroseconds(700);  // Скорость шага (можно уменьшить для ускорения)
    digitalWrite(stepPin, LOW);
    delayMicroseconds(500);
  }
}
