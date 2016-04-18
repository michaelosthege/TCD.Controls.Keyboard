# TCD.Controls.Keyboard
An on screen keyboard for UWP apps on Windows IoT

## Notes

The control is based on the DigitalSignage UWP app from the UWP samples (https://ms-iot.github.io/content/en-US/win10/samples/DigitalSignage.htm).

### Multiple bugs were adressed in comparison to the original
* input injects directly into target TextBox/PasswordBox instead of OutputString property 
* the Content property of the [ key was fixed 
* spacing between keys subtituted with black margin to prevent unwanted unfocusing 
* IsTabStop="False" on all keys causes focus to remain at the TextBox

### New features were introduced
* symbols for Tab, Capslock, Shift, Backspace, Return 
* keyboard layout
  * Shift+SPACE can toggle the keyboard layout between German/English
  * InitialLayout property of the control to ... guess what. 
  * KeyAssignmentSet.cs was restructured to allow for additional keyboard layouts
* IsEnabled property of all keys regulates activation/deactivation when a TextBox is focused/unfocused

### Usage

```XML
<oks:OnScreenKeyBoard x:Name="keyboard" InitialLayout="German" Margin="20" />
```

Register all textboxes and password boxes with the keyboard. (The keyboard subscribes to GotFocus and LostFocus.)

```csharp
// in your MainPage constructor 
keyboard.RegisterTarget(textBox1); 
keyboard.RegisterTarget(textBox2); 
keyboard.RegisterTarget(passwordBox1);
```

### Result

This is what it looks like:

![Screenshot](/images/screenshot1.png) Format: ![a screenshot](url)
