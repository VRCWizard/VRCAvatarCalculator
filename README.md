# VRCAvatarCalculator
Do math with a basic calculator in VRChat. You can interact with the Calculator in VR or with the desktop UI.


![image](https://user-images.githubusercontent.com/101527472/229595934-64864168-13f4-4a41-b421-a5e3c035cda4.png)



There is no unity project included for the actual calculator but you can make your own.
- Get a Calculator model.
- Add Contact Receivers to the buttons with the approriate parameter and collision tag (finger)
    - Example parameter would be ```Calc1``` coresponding pressing the "1" button. All possible contact reciever parameters listed below. 
 - Install [Killfrenzy 1.2.7](https://github.com/killfrenzy96/KillFrenzyAvatarText/releases/tag/1.2.7) avatar text on your avatar and move the constrant to the calculator. (remove the freeze rotation axes)



![image](https://user-images.githubusercontent.com/101527472/229594372-b5796979-f9a8-4a12-81ad-ce36c2405dd4.png)

![image](https://user-images.githubusercontent.com/101527472/229594556-e3065161-13f2-41c2-9958-136b9e32d774.png)

![image](https://user-images.githubusercontent.com/101527472/229597107-71606b10-10dd-41f1-a056-4c712bd91e39.png)



All the possible contact receiver parameters
```
/avatar/parameters/Calc0  
/avatar/parameters/Calc1  
/avatar/parameters/Calc2  
/avatar/parameters/Calc3  
/avatar/parameters/Calc4
/avatar/parameters/Calc5
/avatar/parameters/Calc6
/avatar/parameters/Calc7
/avatar/parameters/Calc8
/avatar/parameters/Calc9
/avatar/parameters/CalcOpenParen    // open parenthesis "("
/avatar/parameters/CalcCloseParen   // close parenthesis ")"
/avatar/parameters/CalcPoint        // decimal point "."
/avatar/parameters/CalcEquals
/avatar/parameters/CalcClear        // Clear will completely erase the current calculation. 
/avatar/parameters/CalcEntryClear   // Clear Entry will only erase the last entered number or operator.
/avatar/parameters/CalcAdd
/avatar/parameters/CalcSub
/avatar/parameters/CalcMult
/avatar/parameters/CalcDiv
```

## Need Help / Have Questions / Wanna make suggestions?
-  Join the [Discord Server](https://discord.gg/YjgR9SWPnW) <br />
  
<a href="https://discord.gg/YjgR9SWPnW"><img src="https://discordapp.com/api/guilds/681732152517591048/widget.png?style=banner2" /></a>
