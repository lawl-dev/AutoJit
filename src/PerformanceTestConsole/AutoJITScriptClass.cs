using AutoJITRuntime;

namespace PerformanceTestConsole
{
    public class AutoJITScriptClass
    {
        private AutoitContext<AutoJITScriptClass> _context;
        private AutoitRuntime<AutoJITScriptClass> _functions;
        public Variant Main()
        {
            ForToNextLooper loopHandler3e770e7116c64c08b00628ecf6418a43;
            Variant v_i = null;
            ForToNextLooper loopHandler1393fdbf427f49de834dea1d50522f22;
            Variant v_j = null;
            v_Chrono = Variant.CreateArray(new Variant[Variant.Create(21)]);
            v_Mess = _functions.Concat(Variant.Create("Timer values"), _context.CRLF);
            loopHandler3e770e7116c64c08b00628ecf6418a43 = new ForToNextLooper(Variant.Create(1), Variant.Create(20), null);
            v_i = loopHandler3e770e7116c64c08b00628ecf6418a43.Index;
            for (; loopHandler3e770e7116c64c08b00628ecf6418a43.MoveNext(); v_i = loopHandler3e770e7116c64c08b00628ecf6418a43.Index)
            {
                v_go = _functions.TimerInit();
                loopHandler1393fdbf427f49de834dea1d50522f22 = new ForToNextLooper(Variant.Create(1), Variant.Create(1337), null);
                v_j = loopHandler1393fdbf427f49de834dea1d50522f22.Index;
                for (; loopHandler1393fdbf427f49de834dea1d50522f22.MoveNext(); v_j = loopHandler1393fdbf427f49de834dea1d50522f22.Index)
                {
                    f_getPrimesTo();
                    ConinueLoop_level_2_count_0:
                    ;
                }

                ExitLooP_level_2_count_0:
                ;
                v_Chrono[v_i] = _functions.TimerDiff(v_go);
                v_Mess = v_Mess.ConcatAssign(_functions.Concat(_functions.Concat(_functions.Concat(_functions.Concat(_functions.Concat(Variant.Create("Pass "), v_i), Variant.Create(" = ")), v_Chrono[v_i]), Variant.Create("ms")), _context.CRLF));
                ConinueLoop_level_1_count_0:
                ;
            }

            ExitLooP_level_1_count_0:
            ;
            f__Report();
            _functions.Sleep(Variant.Create(10000));
            _functions.Exit(0);
            return Variant.Create((object)null);
        }

        Variant v_Chrono = Variant.Create((object)null);
        Variant v_Mess = Variant.Create((object)null);
        Variant v_go = Variant.Create((object)null);
        public Variant f_getPrimesTo()
        {
            _functions.DllCall(Variant.Create("kernel32.dll"), Variant.Create("INT"), Variant.Create("Beep"), Variant.Create("DWORD"), Variant.Create(1000), Variant.Create("DWORD"), Variant.Create(0));
            return Variant.Create((object)null);
        }

        public Variant f__Report()
        {
            v_Mess = v_Mess.ConcatAssign(_context.CRLF);
            v_Mess = v_Mess.ConcatAssign(_functions.Concat(_functions.Concat(_functions.Concat(Variant.Create("Min: "), f__Minn()), Variant.Create("ms")), _context.CRLF));
            v_Mess = v_Mess.ConcatAssign(_functions.Concat(_functions.Concat(_functions.Concat(Variant.Create("Max: "), f__Maxx()), Variant.Create("ms")), _context.CRLF));
            v_Mess = v_Mess.ConcatAssign(_functions.Concat(_functions.Concat(_functions.Concat(Variant.Create("Ave: "), f__Ave()), Variant.Create("ms")), _context.CRLF));
            _functions.ConsoleWrite(_functions.Concat(_functions.ConsoleWrite(_functions.Concat(v_Mess, _context.CRLF)), _context.CRLF));
            return Variant.Create((object)null);
        }

        public Variant f__Maxx()
        {
            Variant v_i = Variant.Create((object)null);
            Variant v_Max = Variant.Create((object)null);
            ForToNextLooper loopHandler17c246bb9e9b41d7898781d9668786db;
            loopHandler17c246bb9e9b41d7898781d9668786db = new ForToNextLooper(Variant.Create(1), _functions.UBound(v_Chrono) - Variant.Create(1), null);
            v_i = loopHandler17c246bb9e9b41d7898781d9668786db.Index;
            for (; loopHandler17c246bb9e9b41d7898781d9668786db.MoveNext(); v_i = loopHandler17c246bb9e9b41d7898781d9668786db.Index)
            {
                if (v_Chrono[v_i] > v_Max)
                {
                    v_Max = v_Chrono[v_i];
                }

                ConinueLoop_level_1_count_0:
                ;
            }

            ExitLooP_level_1_count_0:
            ;
            return v_Max;
            return Variant.Create((object)null);
        }

        public Variant f__Minn()
        {
            Variant v_i = Variant.Create((object)null);
            Variant v_Min = Variant.Create((object)null);
            ForToNextLooper loopHandler65ab46d68c2f404abafc91e898dee63f;
            v_Min = v_Chrono[Variant.Create(1)];
            loopHandler65ab46d68c2f404abafc91e898dee63f = new ForToNextLooper(Variant.Create(1), _functions.UBound(v_Chrono) - Variant.Create(1), null);
            v_i = loopHandler65ab46d68c2f404abafc91e898dee63f.Index;
            for (; loopHandler65ab46d68c2f404abafc91e898dee63f.MoveNext(); v_i = loopHandler65ab46d68c2f404abafc91e898dee63f.Index)
            {
                if (v_Chrono[v_i] < v_Min)
                {
                    v_Min = v_Chrono[v_i];
                }

                ConinueLoop_level_1_count_0:
                ;
            }

            ExitLooP_level_1_count_0:
            ;
            return v_Min;
            return Variant.Create((object)null);
        }

        public Variant f__Ave()
        {
            Variant v_i = Variant.Create((object)null);
            Variant v_Ave = Variant.Create((object)null);
            ForToNextLooper loopHandlerb9cb5b37ec53441398a1fe2d9f9a3c1e;
            v_Ave = Variant.Create(0);
            loopHandlerb9cb5b37ec53441398a1fe2d9f9a3c1e = new ForToNextLooper(Variant.Create(1), _functions.UBound(v_Chrono) - Variant.Create(1), null);
            v_i = loopHandlerb9cb5b37ec53441398a1fe2d9f9a3c1e.Index;
            for (; loopHandlerb9cb5b37ec53441398a1fe2d9f9a3c1e.MoveNext(); v_i = loopHandlerb9cb5b37ec53441398a1fe2d9f9a3c1e.Index)
            {
                v_Ave += v_Chrono[v_i];
                ConinueLoop_level_1_count_0:
                ;
            }

            ExitLooP_level_1_count_0:
            ;
            return v_Ave / v_i;
            return Variant.Create((object)null);
        }

        public Variant f_F()
        {
            Variant v_arr = Variant.Create((object)null);
            v_arr = Variant.CreateArray(new Variant[Variant.Create(1), Variant.Create(2)]);
            return Variant.Create((object)null);
        }

        public AutoJITScriptClass()
        {
            _context = new AutoitContext<AutoJITScriptClass>(this);
            _functions = new AutoitRuntime<AutoJITScriptClass>(_context);
            Main();
        }
    }
}