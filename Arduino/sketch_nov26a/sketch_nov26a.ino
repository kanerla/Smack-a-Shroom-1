// Define button pins
const int butPin1 = 7; // Button 1 connected to digital pin 7
const int butPin2 = 6; // Button 2 connected to digital pin 6
const int butPin3 = 5; // Button 3 connected to digital pin 5
const int buzzerPin = 8; // Buzzer connected to digital pin 8

// Variables for debouncing
unsigned long lastPressTime1 = 0;
unsigned long lastPressTime2 = 0;
unsigned long lastPressTime3 = 0;
const unsigned long debounceDelay = 200; // Milliseconds of debounce delay

void setup() {
  Serial.begin(9600);

  // Set buttons as input with internal pull-up resistors
  pinMode(butPin1, INPUT_PULLUP);
  pinMode(butPin2, INPUT_PULLUP);
  pinMode(butPin3, INPUT_PULLUP);

  // Set buzzer pin as output (optional when using tone, but good practice)
  pinMode(buzzerPin, OUTPUT);
}

void loop() {
  // Check button 1 with debouncing
  if (digitalRead(butPin1) == LOW && (millis() - lastPressTime1) > debounceDelay) {
    Serial.write(1);           // Send value 1 to indicate Mushroom1
    lastPressTime1 = millis(); // Update last press time

    // Activate the buzzer with a high-pitched tone
    playBuzzer(1000, 100); // Play 1000 Hz for 100 ms
  }

  // Check button 2 with debouncing
  if (digitalRead(butPin2) == LOW && (millis() - lastPressTime2) > debounceDelay) {
    Serial.write(2);           // Send value 2 to indicate Mushroom2
    lastPressTime2 = millis(); // Update last press time

    // Activate the buzzer with a different tone
    playBuzzer(1200, 100); // Play 1200 Hz for 100 ms
  }

  // Check button 3 with debouncing
  if (digitalRead(butPin3) == LOW && (millis() - lastPressTime3) > debounceDelay) {
    Serial.write(3);           // Send value 3 to indicate Mushroom3
    lastPressTime3 = millis(); // Update last press time

    // Activate the buzzer with another unique tone
    playBuzzer(800, 100); // Play 800 Hz for 100 ms
  }
}

// Function to play a tone on the buzzer
void playBuzzer(int frequency, int duration) {
  tone(buzzerPin, frequency, duration); // Play tone at specified frequency and duration
  delay(duration);                      // Wait for the tone to complete
  noTone(buzzerPin);                    // Stop the tone
}