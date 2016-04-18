# TCD.Controls.Keyboard
An on screen keyboard for UWP apps on Windows IoT

## Notes

The control is based on the DigitalSignage UWP app from the UWP samples (https://ms-iot.github.io/content/en-US/win10/samples/DigitalSignage.htm).

### Multiple bugs were adressed in comparison to the original
* input injects directly into target TextBox/PasswordBox instead of OutputString property 
* the Content property of the [ key was fixed 
* symbols for Tab, Capslock, Shift, Backspace, Return 
* spacing between keys subtituted with black margin to prevent unwanted unfocusing 
* IsTabStop="False" on all keys causes focus to remain at the TextBox

### New features were introduced
* keyboard layout
** Shift+SPACE can toggle the keyboard layout between German/English
** InitialLayout property of the control to ... guess what.
** KeyAssignmentSet.cs was restructured to allow for additional keyboard layouts
* IsEnabled property of all keys regulates activation/deactivation when a TextBox is focused/unfocused

