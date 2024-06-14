# VpetWorkUpdater
for Vpet Simulator which have many of the outdated work/Study/Play in steam workshop. so this is the solution. All code reference from Chatgpt  
  
So many works/schools are out of dates. Meow Meow can not afford the income and cost. Let's upgrade the salaries to make the job usable meow.  
  
Program with ChatGPT and a cute innocent MeowMeow.  
This Mod is mainly aiming to Upgrade the Works/Studies/Play mods which is out of date.  
To achieve this goal, we need to maximize the salary(MoneyBase) which can fit in the working level(MoneyLevel).  
On the other hand, we also need to rebalance the income to cost ratio(Rel=Get/Spend).  
I will explain “How to do” and “What to do” below.  

1.How to use it?  
  Find the "WorkDataCompile.exe" file location and drag your mod under it then double click on WorkDataCompile.exe to proceed your mod data.  
!!WARNING!!  
  【DO NOT drag】 "WorkDataCompile folder" or "WorkDataCompile.exe" into any 【"System File Location"】,【"..\Steam\steamapps\workshop\content\1920960"】  
  【DO BACKUP】 your Work/Study/Play MOD.  
  【DO drag】 your work/study/play MOD 【Under】WorkDataCompile.exe file Location. "..\Debug\net8.0"  
  (Surely you can drag the whole MOD folder into the folder where WorkDataCompile.exe is, but please always backup your file before doing it)  
  
2.What functions does this Mod have?  
  
	[1] Enter the Money Base Multiplier value(Value1<=1).(it will directly affect the Salary positively(non-linear))  
  After the culculation is over and before the data been output, Value1 will multiplied by the MoneyBase.  
  newMoneyBase = maxMoneyBase x Value1
  (The income money/experience (get)=[(MoneyBase+lv*MoneyLevel)*(1+FinishBonus/2)]/10)
  
	[2] Enter a cutoff value(Value2 >= 0.1) to make the maxMoneyBase lower. Before the culculation been start the maxMoneyBase will minus this margin value which will make the MoneyBase stay inside the legal value.   
  newMoneyBase = ( maxMoneyBase - Value2 ) x Value1   
  
	[3]Setup the minimum Working Level. Making all the works which is below the "newLevelLimit" will increase the Working Level requirement, Salary and etcs.  

	[4]Whether you want to modify the work which is OP/overload/overpower.(Only when Income to Cost Ratio(Rel) <= 1.4)!   
	Enter 【No】 to modify the OP Food.  
	[5] Whether you want to add a Random Money Base Multiplier value(Value3) & Whether you want to modify the working level(LevelLimit) which is greater than LV.555.  
		Enter【11】 to add both functions. (All option: 【00】, 【01】,【11】,【10】).  
	Random Number is create by rolling two twenty sided dice (2D20). Rolling consequence as below:  
   
Roll2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-> MoneyBase x 0.78   
3 ~ 4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-> MoneyBase x 0.92   
5 ~ 9&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-> MoneyBase x 0.95   
10 ~ 15&nbsp;&nbsp;&nbsp;&nbsp;-> MoneyBase x 0.97   
16 ~ 40&nbsp;&nbsp;&nbsp;&nbsp;-> MoneyBase x 1   
  
  newMoneyBase = (( maxMoneyBase - Value2 ) x Value3 ) x Value1
  
  
