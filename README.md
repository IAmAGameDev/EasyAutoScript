# EasyAutoScript

This repository contains the code for a educational new coding language I created called EasyAutoScript.

## To-Do List

`Errors`:

- None currently known.

`Features`:

- [x] Sleep()
- [x] Create a new CustomExceptions for easier debugging
- [x] Add !true and !false handling
- [ ] Add a try block to the Program.cs for file load and custom Exceptions
- [x] Create a BooleanLiteralExpression to handle booleans
- [x] GetAllOpenWindowTitles() - Creating a optional Boolean default: false
- [x] GetOpenWindowTitle()
- [x] GetForeground Application - Console Logs HWND
- [x] Handle line comments // or ; or something
- [x] SetForeground Application - Sets the Foreground Application Based on HWND
- [ ] Get mouse position function (with flag of relative to HWND (ScreenToClient)).
- [ ] Move mouse position function.
- [ ] Send mouse click.
- [ ] Send keyboard/mouse action(s) (SendInput with BlockInput usage/flag)
- [ ] `HARD` List Vars - WIP

  - [ ] Declare a LIST
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

- `//`

  - `[in]` // `Comment`
  - `[out]` Line is skipped from being lexed.

- `Clear()`

  - `[in]` None.
  - `[out]` `console` text cleared.

- `GetAllOpenWindowTitles(optional boolean)`

  - `[in]` Optional `boolean` to display hidden windows overlays.
  - `[out]` `string array` storable into a var or can be used in Write().

- `GetForegroundWindow()`

  - `[in]` None.
  - `[out]` `IntPtr` value of the currently active ForegroundWindow at the time of running this code.

- `GetOpenWindowTitle()`

  - `[in]` None.
  - `[out]` `string` value of what the currently active window title is.

- `SetForegroundWindow(number)`

  - `[in]` `number` value.
  - `[out]` The application sets the Windows foreground window to number.

- `Sleep(number)`

  - `[in]` `number` value.
  - `[out]` Delay before the rest of the code execution.

- `Write(boolean/number/string)`

  - `[in]` `boolean/number/string` value.
  - `[out]` Inputted message displayed displayed to `console`.

- `Var`
  - `var name = value`
    - `[in]` `string` name and `boolean/number/string` value.
    - `[out]` `value` stored in variable `name`.
  - `name = value`
    - `[in]` `string` name and `boolean/number/string` value.
    - `[out]` `value` stored in variable `name` gets updated.
