using System.Text.RegularExpressions;


namespace FoodFileModifier
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Meow Meow Helloooo meow meow want to go hunting~");
            Thread.Sleep(660); // 暫停 0.5 秒

            string rootDirectory = Directory.GetCurrentDirectory(); // 取得當前目錄

            // 提示用戶輸入一個 double 變數
            double MoneyBaseMult = GetMoneyBaseMultiply();

            // 提示用戶輸入一個 double 變數
            double SafeMargins = GetSafeMargins();

            // 提示用戶輸入一個 double 變數
            double MinimLevelLimit = GetMinimLevelLimit();

            // 提示用戶輸入一個 bool 變數
            bool ModOpItme = ModOverPower("不要更動OverPower的工作？(yes/no); Do NOT modify Over Power Work/Study(yes/no): ");

            // 提示用戶輸入兩個 bool 變數
            (bool,bool) RNDandIgHiLevelM = RNDandIgHiLevel("Randomize Min MoneyBase & Do Not Modify LimitLevel > 555. Enter:'1','0','00','01','10','11': ");//(max:16*MinPrice))
            Thread.Sleep(700); // 暫停 0.7 秒

            Console.WriteLine($"Remember to backup any of your file. Or it will be very funny after Meow Meow CountDown for {MoneyBaseMult * 7} sec");
            int MeowCountDown = (int)(MoneyBaseMult * 7000);
            Thread.Sleep(MeowCountDown); // 暫停 MoneyBaseMult*8 秒

            ModifyLPSFiles(rootDirectory, MoneyBaseMult, ModOpItme, SafeMargins, RNDandIgHiLevelM.Item1, RNDandIgHiLevelM.Item2, MinimLevelLimit);
            Console.WriteLine("操作完成。 Operation completed.");

            // 等待用户按下 ESC 键关闭程序
            WaitForExit();
        }
        static double GetMoneyBaseMultiply()
        {
            double MoneyBaseMult = 0.0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("請輸入MoneyBase的倍率(值<=1); Enter the MoneyBase Mutipling Value(NewMoneyBase = MaxMoneyBase x Value)(Value <= 1)：");
                string input = Console.ReadLine();

                if (double.TryParse(input, out MoneyBaseMult))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("請輸入有效的數字。 Please enter a valid number.");
                }
            }

            return MoneyBaseMult;
        }
        static double GetSafeMargins()
        {
            double SafeMargins = 0.0;
            double SafeMarginsI = 0.0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("請輸入薪資減值，避免工作超模;(值>0.1) \nEnter Safety Margins of MoneyBase to prevent work become OverPower(Value >= 0.1)：");
                string input = Console.ReadLine();

                if (double.TryParse(input, out SafeMarginsI))
                {
                    validInput = true;
                    //BottomPrice= Math.Abs(BottomPriceI);
                    SafeMargins = SafeMarginsI;
                }
                else
                {
                    Console.WriteLine("請輸入有效的數字。 Please enter a valid number");
                }
            }

            return SafeMargins;
        }
        static double GetMinimLevelLimit()
        {
            double MinimLevelLimit = 0.0;
            double MinimLevelLimitI = 0.0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("請輸入最低工作等級;(值>0) \nEnter Minimum Working Level Limit(Value >= 0)：");
                string input = Console.ReadLine();

                if (double.TryParse(input, out MinimLevelLimitI))
                {
                    validInput = true;
                    //BottomPrice= Math.Abs(BottomPriceI);
                    MinimLevelLimit = MinimLevelLimitI;
                }
                else
                {
                    Console.WriteLine("請輸入有效的數字。 Please enter a valid number");
                }
            }

            return MinimLevelLimit;
        }

        static bool ModOverPower(string prompt)
        {
            bool validInput = false;
            bool result = false;

            while (!validInput)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "yes" || input == "y" || input == "true" || input == "t" || input == "1" || input == "tr")
                {
                    result = true;
                    validInput = true;
                }
                else if (input == "no" || input == "n" || input == "false" || input == "f" || input == "0" || input == "fa")
                {
                    result = false;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("請輸入 yes 或 no。 Please enter 'yes' or 'no'");
                }
            }

            bool resultb = !result;
            return resultb;
        }
        static (bool,bool) RNDandIgHiLevel(string prompt)
        {
            bool validInput = false;
            bool result1 = false;
            bool result2 = false;

            while (!validInput)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "nono" || input == "nn" || input == "falsefalse" || input == "00" || input == "ff" || input == "0")
                {
                    result1 = false;
                    result2 = false;
                    validInput = true;
                }
                else if (input == "noyes" || input == "ny" || input == "falsetrue" || input == "ft" || input == "01")
                {
                    result1 = false;
                    result2 = true;
                    validInput = true;
                }                
                else if (input == "yesyes" || input == "yy" || input == "truetrue" || input == "tt" || input == "11" || input == "1")
                {
                    result1 = true;
                    result2 = true;
                    validInput = true;
                }                
                else if (input == "yesno" || input == "yn" || input == "truefalse" || input == "ft" || input == "10")
                {
                    result1 = true;
                    result2 = false;
                    validInput = true;
                }                
                else
                {
                    Console.WriteLine("請輸入 0,1,00,01,10,11。 Please enter '0','1','00','01','10','11'");
                }
            }

            return (result1,result2);
        }

        static void ModifyLPSFiles(string directory, double PriceMultiT, bool ModOpItmeT, double SafeMarginsT, bool RNDdiceT, bool IgHiLevelT, double LevelLimitMinimT)
        {
            try
            {
                string[] files = Directory.GetFiles(directory, "*.lps", SearchOption.AllDirectories);
                int Counter = 5;

                foreach (string file in files)
                {
                    if (Counter > 0)
                    {
                        Console.WriteLine($"BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n BackUp Your File. CountDown {(Counter) * 0.8} sec \n");
                        Thread.Sleep(Counter * 850); // 在每次處理後暫停 Counter * 0.8 秒
                        Counter -= 1;
                    }
                    ModifyFileContent(file, PriceMultiT, ModOpItmeT, SafeMarginsT, RNDdiceT, IgHiLevelT, LevelLimitMinimT);
                    Thread.Sleep(70); // 在每次處理後暫停 0.7 秒
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"無法訪問目錄; Cannot access directory: {directory}");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"目錄不存在; Directory does not exist.: {directory}");
            }
        }

        static void ModifyFileContent(string filePath, double PriceMultiIn, bool ModOpItmes, double SafeMargins, bool RNDdice, bool IgHiLevel, double LevelLimitMinim)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                // 使用正则表达式找到并替换价格数值和 Exp 数值
                Regex regexLine = new Regex(@"(?i)(\|MoneyBase#)(-?\d+(\.\d+)?)|(\|MoneyLevel#)(-?\d+(\.\d+)?)|(\|StrengthFood#)(-?\d+(\.\d+)?)|(\|StrengthDrink#)(-?\d+(\.\d+)?)|(\|Feeling#)(-?\d+(\.\d+)?)|(\|Time#)(-?\d+(\.\d+)?)|(\|FinishBonus#)(-?\d+(\.\d+)?)|(\|Type#)(.*?)(:)|(\|name#)(.*?)(:)|(\|LevelLimit#)(-?\d+(\.\d+)?)");

                bool ThisWorkOverLoadCheck = false;
                for (int i = 0; i < lines.Length; i++)
                {
                    double originalMoneyLevel = 0;
                    double originalStrengthFood = 0;
                    double originalStrengthDrink = 0;
                    double originalFeeling = 0;
                    double originalTime = 0;
                    double originalFinishBonus = 0;
                    int originalType = 0;
                    double originalMoneyBase = 0;
                    string originalname = null;
                    double originalLevelLimit = 0;
                    bool CatchName = false;
                    bool CatchMoneyBase = false;
                    bool CatchType = false;

                    string line = lines[i];
                    MatchCollection matches = regexLine.Matches(line);
                    foreach (Match match in matches)
                    {
                        if (match.Groups[4].Success) // 如果匹配到了 Exp，提取其值
                        {
                            double MoneyLevelValue;
                            if (double.TryParse(match.Groups[5].Value, out MoneyLevelValue))
                            {
                                originalMoneyLevel = MoneyLevelValue;

                            }
                            else
                            {
                                Console.WriteLine($"MoneyLevel 值無法解析; MoneyLevel value error: {match.Value}");

                            }
                        }

                        if (match.Groups[7].Success)
                        {
                            double StrengthFoodValue;
                            if (double.TryParse(match.Groups[8].Value, out StrengthFoodValue))
                            {
                                originalStrengthFood = StrengthFoodValue;

                            }
                            else
                            {
                                Console.WriteLine($"StrengthFood 值無法解析; StrengthFood value error: {match.Value}");
                            }
                        }

                        if (match.Groups[10].Success)
                        {
                            double StrengthDrinkValue;
                            if (double.TryParse(match.Groups[11].Value, out StrengthDrinkValue))
                            {
                                originalStrengthDrink = StrengthDrinkValue;

                            }
                            else
                            {
                                Console.WriteLine($"StrengthDrink 值無法解析; StrengthDrink value error: {match.Value}");
                            }
                        }

                        if (match.Groups[13].Success)
                        {
                            double FeelingValue;
                            if (double.TryParse(match.Groups[14].Value, out FeelingValue))
                            {
                                originalFeeling = FeelingValue;

                            }
                            else
                            {
                                Console.WriteLine($"Feeling 值無法解析; Feeling value error: {match.Value}");
                            }
                        }

                        if (match.Groups[16].Success)
                        {
                            double TimeValue;
                            if (double.TryParse(match.Groups[17].Value, out TimeValue))
                            {
                                originalTime = TimeValue;

                            }
                            else
                            {
                                Console.WriteLine($"Time 值無法解析; Time value error: {match.Value}");

                            }
                        }
                        if (match.Groups[19].Success)
                        {
                            double FinishBonusValue;
                            if (double.TryParse(match.Groups[20].Value, out FinishBonusValue))
                            {
                                originalFinishBonus = FinishBonusValue;

                            }
                            else
                            {
                                Console.WriteLine($"FinishBonus 值無法解析; FinishBonus value error: {match.Value}");
                            }
                        }

                        if (match.Groups[22].Success)
                        {
                            string TypeValue = match.Groups[23].Value;
                            string WriteType = null;
                            string Workpattern = @"(?i)Wo(?:r(?:k)?)?";
                            bool WorkTypeMatch = false;
                            WorkTypeMatch = Regex.IsMatch(TypeValue, Workpattern);
                            string Studypattern = @"(?i)St(?:u(?:d(?:y)?)?)?";
                            bool StudyTypeMatch = false;
                            StudyTypeMatch = Regex.IsMatch(TypeValue, Studypattern);
                            string Playpattern = @"(?i)Pl(?:a(?:y)?)?";
                            bool PlayTypeMatch = false;
                            PlayTypeMatch = Regex.IsMatch(TypeValue, Playpattern);

                            if (WorkTypeMatch)
                            {
                                WriteType = @"Work";
                                CatchType = true;
                                Console.WriteLine($" WorkType 為: {WriteType}; ");
                                originalType = 1;
                            }
                            else if (StudyTypeMatch)
                            {
                                WriteType = @"Study";
                                CatchType = true;
                                Console.WriteLine($" WorkType 為: {WriteType}; ");
                                originalType = 2;
                            }
                            else if (PlayTypeMatch)
                            {
                                WriteType = @"Play";
                                CatchType = true;
                                Console.WriteLine($" WorkType 為: {WriteType}; ");
                                originalType = 3;
                            }
                            else
                            {
                                Console.WriteLine($"Type 值無法解析; Type value error: {match.Value}");

                            }
                        }
                        if (match.Groups[25].Success)
                        {
                            originalname = match.Groups[26].Value;
                            CatchName = true;
                        }

                        if (match.Groups[28].Success)
                        {
                            double LevelLimitValue = 0;
                            if (double.TryParse(match.Groups[29].Value, out LevelLimitValue))
                            {
                                originalLevelLimit = LevelLimitValue;
                                //Console.WriteLine($" LevelLimit 值為: {originalMoneyLevel}");
                            }
                            else
                            {
                                Console.WriteLine($"LevelLimit 值無法解析; LevelLimit value error: {match.Value}");
                            }
                        }

                        if (match.Groups[1].Success)
                        {
                            double MoneyBaseValue = 0;
                            if (double.TryParse(match.Groups[2].Value, out MoneyBaseValue))
                            {

                                originalMoneyBase = MoneyBaseValue;
                                CatchMoneyBase = true;
                            }
                            else
                            {
                                Console.WriteLine($"MoneyBase 值無法解析; MoneyBase formation cannot been recognized: {match.Value}");
                            }
                        }
                    }
                    ////////////////////////////Calculate and write///////////////////////////////////
                    bool SalaryReworkSucceed = false;
                    if (CatchName && CatchMoneyBase && CatchType)
                    {

                        double newMoneyBase = 0;
                        double newStrengthFood = 0;
                        double newStrengthDrink = 0;
                        double newFeeling = 0;
                        double newLevelLimit = originalLevelLimit;
                        //double newMoneryLevel = 0;
                        double SalaryMinRatio = 0.75;
                        double SpendW = SpendWork(originalStrengthFood, originalStrengthDrink, originalFeeling, newLevelLimit);

                        double GetW = 0;
                        double SalaryLimit = 0;

                        GetW = GetWorkGain(originalType, originalMoneyBase, originalFinishBonus);

                        double RelWorg = RelWork(GetW, SpendW);
                        Console.WriteLine($"Name:{originalname},Type:{originalType},MoneyBase:{originalMoneyBase},LevelLimit:{originalLevelLimit}," +
                            $"StrengthFood:{originalStrengthFood},StrengthDrink:{originalStrengthDrink},Feeling:{originalFeeling}," +
                            $"\nFinishBonus:{originalFinishBonus},Time:{originalTime},RelWork:{RelWorg:F4},Get{GetW:F4},SP{SpendW:F4}...Modifying>>>");

                        if (originalType == 1)
                        {
                            SalaryLimit = (1.1 * originalLevelLimit + 10);
                        }
                        else if (originalType == 2 || originalType == 3)
                        {
                            SalaryLimit = (1.1 * originalLevelLimit + 10) * 10;
                        }

                        //OverloadCheck&OverloadRepaire
                        if (OverloadChecker(originalLevelLimit, originalFinishBonus, originalType, originalFeeling, originalMoneyBase, RelWorg, SalaryLimit))
                        {ThisWorkOverLoadCheck = true;}
                        else { ThisWorkOverLoadCheck = false; }

                        if(ThisWorkOverLoadCheck && ModOpItmes)
                        {
                            Overloadrepair(originalLevelLimit, originalFinishBonus, originalType, originalFeeling, originalMoneyBase, RelWorg, SalaryLimit);
                            if(RelWorg<0)
                            {
                                if(originalStrengthFood < 0)
                                { originalStrengthFood *= (-1); }
                                if(originalStrengthDrink < 0)
                                { originalStrengthDrink *= (-1); }
                            }
                        }

                        if (originalType == 3 && originalFeeling > 0)
                        {
                            originalFeeling *= (-1);//旧版本代码兼容
                            SpendW = SpendWork(originalStrengthFood, originalStrengthDrink, originalFeeling, originalLevelLimit);
                        }
                        if(LevelLimitMinim > originalLevelLimit)
                        {
                            newLevelLimit = LevelLimitMinim;
                        }
                        else
                        {
                            newLevelLimit = originalLevelLimit;
                        }

                        if (originalType == 1)
                        {
                            SalaryLimit = (1.1 * newLevelLimit + 10);
                        }
                        else if (originalType == 2 || originalType == 3)
                        {
                            SalaryLimit = (1.1 * newLevelLimit + 10) * 10;
                        }
                        double MoneyDiceMult = 1;
                        MoneyDiceMult = DiceMoneyMulti(RNDdice);
                        ////////////////////////////////////////////////Compile Start///////////////////////////////////////////////////////
                        //1.情況一:Work Salary too low
                        if (((originalType == 1 || originalType == 2) || (originalFeeling < 0 && originalType == 3) )
                            && originalFinishBonus <= 2 && originalMoneyBase < ((SalaryLimit - SafeMargins) * SalaryMinRatio) 
                            && ((RelWorg >= 0 && RelWorg <= 1.4) || ModOpItmes) && (!(IgHiLevel) || originalLevelLimit <= 555))
                        {
                            //New MoneyBase
                            newMoneyBase = (SalaryLimit - SafeMargins)* MoneyDiceMult;
                            double RelWNew;
                            RelWNew = RelWork(GetWorkGain(originalType, newMoneyBase, originalFinishBonus), 
                                SpendWork(originalStrengthFood, originalStrengthDrink, originalFeeling, newLevelLimit));
                            double counterlimit = 6;
                            if (newLevelLimit <= 10) { counterlimit = 7; }
                            else if (newLevelLimit <= 50) { counterlimit = 13; }
                            else if (newLevelLimit <= 99) { counterlimit = 25; }
                            else if (newLevelLimit <= 199) { counterlimit = 41; }
                            double newMoneyBaseT = newMoneyBase;
                            //1-1 Feeling increase x1.5, StrengthDrink >0 change, <=0 remain; StrengthFood >0 change, <=0 remain. 
                            //for MoneyBase x Ratio
                            int RatioCounter = 0;
                            for (int CunFeeMon = 0; CunFeeMon <= 8; CunFeeMon++)
                            {
                                newFeeling = originalFeeling;
                                newStrengthDrink = originalStrengthDrink;
                                newStrengthFood = originalStrengthFood;

                                double GetWLoop = GetWorkGain(originalType, newMoneyBaseT, originalFinishBonus);

                                if (RelWNew > 1.4)
                                {
                                    int countfour = 1;
                                    int countfive = 1;
                                    //for Feeling, Drink, Food increase
                                    for (int CounterF = 0; CounterF <= counterlimit; CounterF++)
                                    {

                                        RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                        if (RelWNew <= 1.4) { break; }

                                        if (originalFeeling > 0)
                                        {
                                            //New Feeling(1)
                                            newFeeling += 0.5;
                                            RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                            if (RelWNew <= 1.4) { break; }
                                        }
                                        if (originalStrengthDrink > 0)
                                        {
                                            //New StrengthDrink(1)
                                            newStrengthDrink += 0.5;
                                            RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                            if (RelWNew <= 1.4) { break; }

                                            if (countfive == 5)
                                            {
                                                //New StrengthDrink(2)
                                                newStrengthDrink += 0.5;
                                                countfive = 0;
                                                RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                                if (RelWNew <= 1.4) { break; }
                                            }
                                        }

                                        if (countfour == 4 && originalFeeling > 0)
                                        {
                                            //New Feeling(2)
                                            newFeeling += 0.5;
                                            countfour = 0;
                                            RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                            if (RelWNew <= 1.4) { break; }
                                        }

                                        if (originalStrengthFood > 0)
                                        {
                                            //New StrengthFood(1)
                                            newStrengthFood += 0.5;
                                            RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                            if (RelWNew <= 1.4) { break; }
                                        }

                                        countfour++;
                                        countfive++;

                                    }//for Feeling, Drink, Food increase
                                    countfour = 1;
                                    countfive = 1;
                                }//if RelWNew>1.4
                                else if (RelWNew <= 1.4)
                                {
                                    break;
                                }

                                RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                if (RelWNew <= 1.4)
                                {
                                    newMoneyBase = newMoneyBaseT;
                                    break;
                                }
                                newMoneyBaseT = newMoneyBase * (1 - ((RatioCounter + 1) / 2 * 0.1));
                                RatioCounter++;

                            }//for MoneyBase*Ratio

                            //1-2 Solution1 Not Work (ex:Level too High)
                            if (RelWNew > 1.4)
                            {
                                Console.WriteLine($"This work is Out of Ranges, active the rough modify mode.");
                                newFeeling = originalFeeling;
                                newStrengthDrink = originalStrengthDrink;
                                newStrengthFood = originalStrengthFood;
                                double GetWLoop = GetWorkGain(originalType, newMoneyBase, originalFinishBonus);
                                int countfour = 1;
                                int countfive = 1;

                                for (int CounterFF = 0; CounterFF <= 9; CounterFF++)
                                {
                                    RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                    if (RelWNew <= 1.4) { break; }

                                    if(originalFeeling > 0)
                                    {
                                        //New Feeling(1)
                                        newFeeling += 5;
                                        RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                        if (RelWNew <= 1.4) { break; }
                                    }
                                    //New StrengthDrink(1)
                                    newStrengthDrink += 5;
                                    RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                    if (RelWNew <= 1.4) { break; }
                                    //New StrengthFood(1)
                                    newStrengthFood += 5;
                                    RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                    if (RelWNew <= 1.4) { break; }

                                    if (originalFeeling > 0 && countfour == 4)
                                    {
                                        //New Feeling(2)
                                        newFeeling += 5;
                                        RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                        if (RelWNew <= 1.4) { break; }
                                        //New Feeling(3)
                                        newFeeling += 5;
                                        RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                        if (RelWNew <= 1.4) { break; }

                                        countfour = 0;
                                    }

                                    //New StrengthDrink(2)
                                    newStrengthDrink += 5;
                                    RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                    if (RelWNew <= 1.4) { break; }
                                    //New StrengthFood(2)
                                    newStrengthFood += 5;
                                    RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                    if (RelWNew <= 1.4) { break; }

                                    if (originalFeeling > 0)
                                    {
                                        //New Feeling(4)
                                        newFeeling += 5;
                                        RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                        if (RelWNew <= 1.4) { break; }
                                    }

                                    if(countfive == 5)
                                    {
                                        //New StrengthDrink(3)
                                        newStrengthDrink += 5;
                                        RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                        if (RelWNew <= 1.4) { break; }
                                        //New StrengthDrink(4)
                                        newStrengthDrink += 5;
                                        RelWNew = RelWork(GetWLoop, SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                                        if (RelWNew <= 1.4) { break; }

                                        countfive = 0;
                                    }

                                    countfour++;
                                    countfive++;
                                }//for Force Raise: Feeling Drink Food cost
                            }
                            //1-3 SalaryReworkSucceed check
                            RelWNew = RelWork(GetWorkGain(originalType, newMoneyBase, originalFinishBonus)
                                , SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                            if (RelWNew <= 1.4) { SalaryReworkSucceed = true; }
                            else { SalaryReworkSucceed = false; }
                        } //Salary Increased done

                        newMoneyBase *= PriceMultiIn;

                        double RelPrint= RelWork(GetWorkGain(originalType, newMoneyBase, originalFinishBonus)
                                , SpendWork(newStrengthFood, newStrengthDrink, newFeeling, newLevelLimit));
                        //Regex regex = new Regex(@"\|price#(-?\d+(\.\d+)?)");
                        if (SalaryReworkSucceed && (RelWorg <= 1.4 || ModOpItmes))
                        {
                            Console.WriteLine($"Name:{originalname},Type:{originalType},MoneyBase:{newMoneyBase:F1},LevelLimit:{newLevelLimit}," +
                                $"StrengthFood:{newStrengthFood:F1},StrengthDrink:{newStrengthDrink:F1},Feeling:{newFeeling:F1}," +
                                $"\nFinishBonus:{originalFinishBonus},Time:{originalTime},RelWork:{RelPrint:F4}");
                            // 寫入修改後的內容
                            line = regexLine.Replace(line, m => m.Groups[1].Success ? $"|MoneyBase#{newMoneyBase:F1}" : m.Value);
                            line = regexLine.Replace(line, m => m.Groups[7].Success ? $"|StrengthFood#{newStrengthFood:F1}" : m.Value);
                            // Check if the line contains the replaced pattern
                            if (!line.Contains($"|StrengthFood#{newStrengthFood:F1}"))
                            {
                                line += $"StrengthFood#{newStrengthFood:F1}:|";
                            }

                            line = regexLine.Replace(line, m => m.Groups[10].Success ? $"|StrengthDrink#{newStrengthDrink:F1}" : m.Value);
                            // Check if the line contains the replaced pattern
                            if (!line.Contains($"|StrengthDrink#{newStrengthDrink:F1}"))
                            {
                                line += $"StrengthDrink#{newStrengthDrink:F1}:|";
                            }

                            line = regexLine.Replace(line, m => m.Groups[13].Success ? $"|Feeling#{newFeeling:F1}" : m.Value);
                            if (!line.Contains($"|Feeling#{newFeeling:F1}"))
                            {
                                line += $"Feeling#{newFeeling:F1}:|";
                            }

                            line = regexLine.Replace(line, m => m.Groups[28].Success ? $"|LevelLimit#{newLevelLimit}" : m.Value);
                            if (!line.Contains($"|LevelLimit#{newLevelLimit}"))
                            {
                                line += $"LevelLimit#{newLevelLimit}:|";
                            }

                            // 将修改后的行写回文件对应的行
                            lines[i] = line;
                        }
                        else if(ThisWorkOverLoadCheck && !(ModOpItmes))
                        {
                            Console.WriteLine($"This work is OverPower.");
                        }
                        else if(!SalaryReworkSucceed)
                        {
                            Console.WriteLine($"This work is out of tuning range.");
                        }
                        else { Console.WriteLine($"This work can't be analysis for some unknown reason."); }

                        CatchMoneyBase = false;
                        CatchName = false;
                        CatchType = false;
                    }
                    else
                    { //Console.WriteLine($"Name or MoneyBase or Type catch fail"); 
                    }
                }
                // 将修改后的所有行写回文件
                File.WriteAllLines(filePath, lines);

                Console.WriteLine($"已修改檔案; File has been modified.: {filePath}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"找不到檔案; File not found.: {filePath}");
            }
            catch (IOException)
            {
                Console.WriteLine($"無法讀寫檔案; Cannot read or write file.: {filePath}");
            }
            catch (FormatException)
            {
                Console.WriteLine($"價格或 Exp 等格式錯誤; Format error for Price, Exp, etcs...: {filePath}");
            }
        }

        /// 工作获取效率
        public static double GetWorkGain(int WorkType, double MoneyBase, double FinishBonus)
        {
            if (WorkType == 1)
            { return (MathPow(Math.Abs(MoneyBase) * (1 + FinishBonus / 2) + 1, 1.25)); }
            else if (WorkType == 2 || WorkType == 3)
            { return (MathPow((Math.Abs(MoneyBase) * (1 + FinishBonus / 2) + 1) / 10, 1.25)); }
            else
            { return (0); }
        }

        /// <returns>工作花费效率</returns>
        public static double SpendWork(double originalStrengthFood, double originalStrengthDrink, double originalFeeling, double originalLevelLimit)
        {
            return ((MathPow(originalStrengthFood, 1.5) / 3 + MathPow(originalStrengthDrink, 1.5) / 4 + MathPow(originalFeeling, 1.5) / 4 +
                originalLevelLimit / 10.0 + MathPow(originalStrengthFood + originalStrengthDrink + originalFeeling, 1.5) / 10) * 3);
        }

        public static bool OverloadChecker(double originalLevelLimit, double originalFinishBonus, double originalType, double originalFeeling, double originalMoneyBase, double originalRel, double SalaryLimit)
        {
            if (originalLevelLimit < 0)
            { return (true); }
            if (originalFinishBonus < 0)
            { return (true); }
            if (originalType == 3 && originalFeeling > 0)
            { return (true); }
            if (originalFinishBonus > 2)
            { return (true); }
            if (originalRel < 0)
            { return (true); }
            if (Math.Abs(originalMoneyBase) > SalaryLimit) //等级获取速率限制
            { return (true); }
            return (originalRel > 1.4); // 推荐rel为1左右 超过1.3就是超模
        }
        public static bool Overloadrepair(double originalLevelLimit, double originalFinishBonus, double originalType, double originalFeeling, double originalMoneyBase, double originalRel, double SalaryLimit)
        {
            if (originalLevelLimit < 0)
            { originalLevelLimit = 0; }
            if (originalFinishBonus < 0)
            { originalFinishBonus = 0; }
            if (originalType == 3 && originalFeeling > 0)
            { originalFeeling *= (-1); }
            if (originalFinishBonus > 2)
            { originalFinishBonus = 2; }
            return (true);
        }

        public static double RelWork(double Get, double Spend)
        {
            return (Get / Spend);
        }

        /// <summary>
        /// 求幂(带符号)
        /// </summary>
        public static double MathPow(double value, double pow)
        {
            return (Math.Pow(Math.Abs(value), pow) * Math.Sign(value));
        }

        /// <summary>
        /// Random Dice 2D20
        /// </summary>
        public static double DiceMoneyMulti(bool RNDdice)
        {
            double PriceDice = 1;
            if (RNDdice)
            {
                //2.情況二:RandomDice
                Random rnd = new Random();
                // 產生一個隨機數，範圍在 1 到 20 之間
                int Dice20D1 = rnd.Next(1, 21);
                int Dice20D2 = rnd.Next(1, 21);
                if (Dice20D1 + Dice20D2 == 2)
                {
                    PriceDice = 0.78;
                }
                else if (Dice20D1 + Dice20D2 > 2 && Dice20D1 + Dice20D2 <= 4)
                {
                    PriceDice = 0.92;
                }
                else if (Dice20D1 + Dice20D2 > 4 && Dice20D1 + Dice20D2 <= 9)
                {
                    PriceDice = 0.95;
                }
                else if (Dice20D1 + Dice20D2 > 9 && Dice20D1 + Dice20D2 <= 15)
                {
                    PriceDice = 0.97;
                }
                else if (Dice20D1 + Dice20D2 > 15 && Dice20D1 + Dice20D2 <= 26)
                {
                    PriceDice = 1;
                }
                else if (Dice20D1 + Dice20D2 > 26 && Dice20D1 + Dice20D2 <= 32)
                {
                    PriceDice = 1;
                }
                else if (Dice20D1 + Dice20D2 > 32 && Dice20D1 + Dice20D2 <= 36)
                {
                    PriceDice = 1;
                }
                else if (Dice20D1 + Dice20D2 > 36 && Dice20D1 + Dice20D2 <= 39)
                {
                    PriceDice = 1;
                }
                else if (Dice20D1 + Dice20D2 == 40)
                {
                    PriceDice = 1;
                }
                else
                {
                    PriceDice = 1;
                }
            }
            return (PriceDice);
        }

        static void WaitForExit()
        {
            Console.WriteLine("Program has finished running.Press ESC key to continue...(Escape key);程式執行結束，請按下ESC按鈕以繼續");

            DateTime lastKeyPressTime = DateTime.Now;

            // 等待用户按下 ESC 键
            while (true)
            {
                Thread.Sleep(400);
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    else
                    {
                        lastKeyPressTime = DateTime.Now;
                    }
                }
                // 檢查是否已經超過 26 秒沒有按鍵輸入
                if ((DateTime.Now - lastKeyPressTime).TotalMilliseconds >= 26000)
                {
                    break;
                }

            }

            Console.WriteLine("Program has been closed. 程式已關閉。");
        }
    }
}
