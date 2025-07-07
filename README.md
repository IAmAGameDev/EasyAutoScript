# My Portfolio Website

This repository contains the code for a educational new coding language I created called EasyAutoScript.

## To-Do List

- [x] Create a BooleanLiteralExpression to handle booleans
- [x] GetAllOpenWindowTitles() - Creating a optional Boolean default: false
- [x] Clear()
- [x] Write() - Accept Boolean to just debug true
- [x] GetOpenWindowTitle()
- [x] GetForeground Application - Console Logs HWND
- [ ] Handle line comments // or ; or something
- [ ] SetForeground Application - Sets the Foreground Application Based on HWND
- [ ] Get mouse position function (with flag of relative to HWND (ScreenToClient)).
- [ ] Move mouse position function.
- [ ] Send mouse click.
- [ ] Send keyboard/mouse action(s) (SendInput with BlockInput usage/flag)
- [x] `HARD` Add Var handling (Boolean/String/Int)
- [ ] `HARD` List Vars - WIP

  - [x] Declare a LIST
  - [ ] LIST[0]
  - [ ] LIST[0] = 1
  - [ ] LIST.Insert(0, Value)
  - [ ] LIST.Add(Value)
  - [ ] List.RemoveAt(0)
  - [ ] List.Remove()
  - [ ] List.Clear()

- [ ] `HARD` If
- [ ] `HARD` For Loop
- [ ] `HARD` While Loop
- [ ] `HARD` Arrays
- [ ] `HARD` ForEach Loop

## Documentation

As this project is so small here is the current list of commands that is accepted:

- `Ctrl C`

  - [in] None
  - [out] Cancels all code execution (interrupt signal).

- `Clear()`

  - [in] None
  - [out] Clears the console screen.

- `GetAllOpenWindowsTitles(includeHidden = false)`

  - [in] Optional _Boolean_ - If true, includes includes overlays and hidden process windows (e.g., Nvidia overlays, settings).
  - [out] Outputs each window title individually to the console.

- `GetOpenWindowTitle()`

  - [in] None
  - [out] Outputs the title of the currently active visible window to the console.

- `GetForegroundWindow()`

  - [in] None
  - [out] Outputs the IntPtr HWND of the current active visible window to the console.

- `SetForegroundWindow("WindowTitle")`

  - [in] _String_ - The message to output.
  - [out] Sets the window _"WindowTitle"_ as the Foreground Application on the device.

- `Sleep(Milliseconds)`

  - [in] _Double_ – Duration to pause in milliseconds.
  - [out] None – Execution is paused for the given duration.

- `Write("Message")`
  - [in] _String_ - The message to output.
  - [out] Prints the message to the console.
