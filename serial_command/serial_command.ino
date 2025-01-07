#include <SoftwareSerial.h>

int mosfetPin = 2;
bool dataReceived = false; 

void setup() {
  Serial.begin(9600);
  pinMode(mosfetPin, OUTPUT);  
  digitalWrite(mosfetPin, LOW); 
}

void loop() {
  if (Serial.available() > 0) {
    
    String strInt = Serial.readStringUntil('\n');
    int converted_value_ms = strInt.toInt() * 1000; // 0 if string is not a valid int

    digitalWrite(mosfetPin, HIGH);
    delay(converted_value_ms);
    
    digitalWrite(mosfetPin, LOW);
    // Ensure the state change happens only once per message
    while (Serial.available() > 0) {
      Serial.read();  // Clear any remaining data in the buffer
    }
  }
  
  delay(1000);
}
