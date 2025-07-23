# EasyAutoScript

This repository contains the code for a educational new coding language I created called EasyAutoScript.

## To-Do List

`Errors`:

- [ ] Cannot pass a number value into Write Statements doesn't return a ( bracket Token
- [ ] Might need to use PeekNext for decimals once implemented so its not 123. and has a number after the .
- [ ] Write(123.123) thinks the tokens are:
  - Write "Write" Write 1
  - OpenParenthesis "(" 1
  - Number "123" 123 1
  - Number "123" 123 1
  - EOF "EOF" 1

`Features`:

- [ ] Documentation
- [ ] Create a new ParseException for easier debugging
- [ ] Make the write statement handle booleans (will need some magic as if I remember correctly it breaks)
- [ ] Add a try block to the Program.cs for file load etc
- [ ] Create a BooleanLiteralExpression to handle booleans
- [ ] GetAllOpenWindowTitles() - Creating a optional Boolean default: false
- [ ] Clear()
- [ ] Write() - Accept Boolean to just debug true
- [ ] GetOpenWindowTitle()
- [ ] GetForeground Application - Console Logs HWND
- [ ] Handle line comments // or ; or something
- [ ] SetForeground Application - Sets the Foreground Application Based on HWND
- [ ] Get mouse position function (with flag of relative to HWND (ScreenToClient)).
- [ ] Move mouse position function.
- [ ] Send mouse click.
- [ ] Send keyboard/mouse action(s) (SendInput with BlockInput usage/flag)
- [ ] `HARD` Add Var handling (Boolean/String/Int)
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
