using AutoJITRuntime;

namespace AutoJITScript
{
    public class AutoJITScriptClass
    {
        private AutoitContext<AutoJITScriptClass> _context;
        private AutoitRuntime<AutoJITScriptClass> _functions;
        public Variant Main()
        {
            ForToNextLooper loopHandler482f9513b1024c1a85f3237c94c54fad;
            Variant v_lol = null;
            Variant v_key = Variant.Create((object)null);
            Variant v_message = Variant.Create((object)null);
            Variant v_ciphertext = Variant.Create((object)null);
            Variant v_recovered_message = Variant.Create((object)null);
            v_timer = _functions.TimerInit();
            loopHandler482f9513b1024c1a85f3237c94c54fad = new ForToNextLooper(Variant.Create(0), Variant.Create(1337), null);
            v_lol = loopHandler482f9513b1024c1a85f3237c94c54fad.Index;
            for (; loopHandler482f9513b1024c1a85f3237c94c54fad.MoveNext(); v_lol = loopHandler482f9513b1024c1a85f3237c94c54fad.Index)
            {
                v_key = Variant.Create("8787878787878787");
                v_message = Variant.Create("Supercalifradialisticexpialidocious");
                v_ciphertext = f_des(v_key, v_message, Variant.Create(1));
                v_recovered_message = f_des(v_key, v_ciphertext, Variant.Create(0));
            ConinueLoop_level_1_count_0:
                ;
            }

        ExitLooP_level_1_count_0:
            ;
            v_diff = _functions.TimerDiff(v_timer);
            _functions.ConsoleWrite(v_diff);
            _functions.Exit(v_diff);
            return Variant.Create((object)null);
        }

        Variant v_timer = Variant.Create((object)null);
        Variant v_diff = Variant.Create((object)null);
        public Variant f_des(Variant v_key, Variant v_message, Variant v_encrypt, Variant v_mode = null, Variant v_iv = null)
        {
            Variant v_spfunction1 = Variant.Create((object)null);
            Variant v_spfunction2 = Variant.Create((object)null);
            Variant v_spfunction3 = Variant.Create((object)null);
            Variant v_spfunction4 = Variant.Create((object)null);
            Variant v_spfunction5 = Variant.Create((object)null);
            Variant v_spfunction6 = Variant.Create((object)null);
            Variant v_spfunction7 = Variant.Create((object)null);
            Variant v_spfunction8 = Variant.Create((object)null);
            Variant v_masks = Variant.Create((object)null);
            Variant v_fill = Variant.Create((object)null);
            Variant v_keys = Variant.Create((object)null);
            Variant v_m = Variant.Create((object)null);
            Variant v_chunk = Variant.Create((object)null);
            Variant v_iterations = Variant.Create((object)null);
            Variant v_looping = Variant.Create((object)null);
            ForToNextLooper loopHandler04edc183cb6f46f5b364670bbfa31989;
            Variant v_i = null;
            Variant v_len = Variant.Create((object)null);
            Variant v_result = Variant.Create((object)null);
            Variant v_tempresult = Variant.Create((object)null);
            Variant v_cbcleft = Variant.Create((object)null);
            Variant v_cbcright = Variant.Create((object)null);
            Variant v_left = Variant.Create((object)null);
            Variant v_right = Variant.Create((object)null);
            Variant v_cbcleft2 = Variant.Create((object)null);
            Variant v_cbcright2 = Variant.Create((object)null);
            Variant v_temp = Variant.Create((object)null);
            ForToNextLooper loopHandler962c28090bc448ba89a8e068435392cd;
            Variant v_j = null;
            Variant v_endloop = Variant.Create((object)null);
            Variant v_loopinc = Variant.Create((object)null);
            Variant v_right1 = Variant.Create((object)null);
            Variant v_right2 = Variant.Create((object)null);
            if (v_iv == null)
                v_iv = Variant.Create("");
            if (v_mode == null)
                v_mode = Variant.Create(0);
            v_spfunction1 = new Variant[Variant.Create(64)];
            v_spfunction1.InitArray(new Variant[]
            {
            Variant.Create(16843776), Variant.Create(0), Variant.Create(65536), Variant.Create(16843780), Variant.Create(16842756), Variant.Create(66564), Variant.Create(4), Variant.Create(65536), Variant.Create(1024), Variant.Create(16843776), Variant.Create(16843780), Variant.Create(1024), Variant.Create(16778244), Variant.Create(16842756), Variant.Create(16777216), Variant.Create(4), Variant.Create(1028), Variant.Create(16778240), Variant.Create(16778240), Variant.Create(66560), Variant.Create(66560), Variant.Create(16842752), Variant.Create(16842752), Variant.Create(16778244), Variant.Create(65540), Variant.Create(16777220), Variant.Create(16777220), Variant.Create(65540), Variant.Create(0), Variant.Create(1028), Variant.Create(66564), Variant.Create(16777216), Variant.Create(65536), Variant.Create(16843780), Variant.Create(4), Variant.Create(16842752), Variant.Create(16843776), Variant.Create(16777216), Variant.Create(16777216), Variant.Create(1024), Variant.Create(16842756), Variant.Create(65536), Variant.Create(66560), Variant.Create(16777220), Variant.Create(1024), Variant.Create(4), Variant.Create(16778244), Variant.Create(66564), Variant.Create(16843780), Variant.Create(65540), Variant.Create(16842752), Variant.Create(16778244), Variant.Create(16777220), Variant.Create(1028), Variant.Create(66564), Variant.Create(16843776), Variant.Create(1028), Variant.Create(16778240), Variant.Create(16778240), Variant.Create(0), Variant.Create(65540), Variant.Create(66560), Variant.Create(0), Variant.Create(16842756)}

            );
            v_spfunction2 = new Variant[Variant.Create(64)];
            v_spfunction2.InitArray(new Variant[]
            {
            Variant.Create(-2146402272), Variant.Create(-2147450880), Variant.Create(32768), Variant.Create(1081376), Variant.Create(1048576), Variant.Create(32), Variant.Create(-2146435040), Variant.Create(-2147450848), Variant.Create(-2147483616), Variant.Create(-2146402272), Variant.Create(-2146402304), Variant.Create(-2147483648), Variant.Create(-2147450880), Variant.Create(1048576), Variant.Create(32), Variant.Create(-2146435040), Variant.Create(1081344), Variant.Create(1048608), Variant.Create(-2147450848), Variant.Create(0), Variant.Create(-2147483648), Variant.Create(32768), Variant.Create(1081376), Variant.Create(-2146435072), Variant.Create(1048608), Variant.Create(-2147483616), Variant.Create(0), Variant.Create(1081344), Variant.Create(32800), Variant.Create(-2146402304), Variant.Create(-2146435072), Variant.Create(32800), Variant.Create(0), Variant.Create(1081376), Variant.Create(-2146435040), Variant.Create(1048576), Variant.Create(-2147450848), Variant.Create(-2146435072), Variant.Create(-2146402304), Variant.Create(32768), Variant.Create(-2146435072), Variant.Create(-2147450880), Variant.Create(32), Variant.Create(-2146402272), Variant.Create(1081376), Variant.Create(32), Variant.Create(32768), Variant.Create(-2147483648), Variant.Create(32800), Variant.Create(-2146402304), Variant.Create(1048576), Variant.Create(-2147483616), Variant.Create(1048608), Variant.Create(-2147450848), Variant.Create(-2147483616), Variant.Create(1048608), Variant.Create(1081344), Variant.Create(0), Variant.Create(-2147450880), Variant.Create(32800), Variant.Create(-2147483648), Variant.Create(-2146435040), Variant.Create(-2146402272), Variant.Create(1081344)}

            );
            v_spfunction3 = new Variant[Variant.Create(64)];
            v_spfunction3.InitArray(new Variant[]
            {
            Variant.Create(520), Variant.Create(134349312), Variant.Create(0), Variant.Create(134348808), Variant.Create(134218240), Variant.Create(0), Variant.Create(131592), Variant.Create(134218240), Variant.Create(131080), Variant.Create(134217736), Variant.Create(134217736), Variant.Create(131072), Variant.Create(134349320), Variant.Create(131080), Variant.Create(134348800), Variant.Create(520), Variant.Create(134217728), Variant.Create(8), Variant.Create(134349312), Variant.Create(512), Variant.Create(131584), Variant.Create(134348800), Variant.Create(134348808), Variant.Create(131592), Variant.Create(134218248), Variant.Create(131584), Variant.Create(131072), Variant.Create(134218248), Variant.Create(8), Variant.Create(134349320), Variant.Create(512), Variant.Create(134217728), Variant.Create(134349312), Variant.Create(134217728), Variant.Create(131080), Variant.Create(520), Variant.Create(131072), Variant.Create(134349312), Variant.Create(134218240), Variant.Create(0), Variant.Create(512), Variant.Create(131080), Variant.Create(134349320), Variant.Create(134218240), Variant.Create(134217736), Variant.Create(512), Variant.Create(0), Variant.Create(134348808), Variant.Create(134218248), Variant.Create(131072), Variant.Create(134217728), Variant.Create(134349320), Variant.Create(8), Variant.Create(131592), Variant.Create(131584), Variant.Create(134217736), Variant.Create(134348800), Variant.Create(134218248), Variant.Create(520), Variant.Create(134348800), Variant.Create(131592), Variant.Create(8), Variant.Create(134348808), Variant.Create(131584)}

            );
            v_spfunction4 = new Variant[Variant.Create(64)];
            v_spfunction4.InitArray(new Variant[]
            {
            Variant.Create(8396801), Variant.Create(8321), Variant.Create(8321), Variant.Create(128), Variant.Create(8396928), Variant.Create(8388737), Variant.Create(8388609), Variant.Create(8193), Variant.Create(0), Variant.Create(8396800), Variant.Create(8396800), Variant.Create(8396929), Variant.Create(129), Variant.Create(0), Variant.Create(8388736), Variant.Create(8388609), Variant.Create(1), Variant.Create(8192), Variant.Create(8388608), Variant.Create(8396801), Variant.Create(128), Variant.Create(8388608), Variant.Create(8193), Variant.Create(8320), Variant.Create(8388737), Variant.Create(1), Variant.Create(8320), Variant.Create(8388736), Variant.Create(8192), Variant.Create(8396928), Variant.Create(8396929), Variant.Create(129), Variant.Create(8388736), Variant.Create(8388609), Variant.Create(8396800), Variant.Create(8396929), Variant.Create(129), Variant.Create(0), Variant.Create(0), Variant.Create(8396800), Variant.Create(8320), Variant.Create(8388736), Variant.Create(8388737), Variant.Create(1), Variant.Create(8396801), Variant.Create(8321), Variant.Create(8321), Variant.Create(128), Variant.Create(8396929), Variant.Create(129), Variant.Create(1), Variant.Create(8192), Variant.Create(8388609), Variant.Create(8193), Variant.Create(8396928), Variant.Create(8388737), Variant.Create(8193), Variant.Create(8320), Variant.Create(8388608), Variant.Create(8396801), Variant.Create(128), Variant.Create(8388608), Variant.Create(8192), Variant.Create(8396928)}

            );
            v_spfunction5 = new Variant[Variant.Create(64)];
            v_spfunction5.InitArray(new Variant[]
            {
            Variant.Create(256), Variant.Create(34078976), Variant.Create(34078720), Variant.Create(1107296512), Variant.Create(524288), Variant.Create(256), Variant.Create(1073741824), Variant.Create(34078720), Variant.Create(1074266368), Variant.Create(524288), Variant.Create(33554688), Variant.Create(1074266368), Variant.Create(1107296512), Variant.Create(1107820544), Variant.Create(524544), Variant.Create(1073741824), Variant.Create(33554432), Variant.Create(1074266112), Variant.Create(1074266112), Variant.Create(0), Variant.Create(1073742080), Variant.Create(1107820800), Variant.Create(1107820800), Variant.Create(33554688), Variant.Create(1107820544), Variant.Create(1073742080), Variant.Create(0), Variant.Create(1107296256), Variant.Create(34078976), Variant.Create(33554432), Variant.Create(1107296256), Variant.Create(524544), Variant.Create(524288), Variant.Create(1107296512), Variant.Create(256), Variant.Create(33554432), Variant.Create(1073741824), Variant.Create(34078720), Variant.Create(1107296512), Variant.Create(1074266368), Variant.Create(33554688), Variant.Create(1073741824), Variant.Create(1107820544), Variant.Create(34078976), Variant.Create(1074266368), Variant.Create(256), Variant.Create(33554432), Variant.Create(1107820544), Variant.Create(1107820800), Variant.Create(524544), Variant.Create(1107296256), Variant.Create(1107820800), Variant.Create(34078720), Variant.Create(0), Variant.Create(1074266112), Variant.Create(1107296256), Variant.Create(524544), Variant.Create(33554688), Variant.Create(1073742080), Variant.Create(524288), Variant.Create(0), Variant.Create(1074266112), Variant.Create(34078976), Variant.Create(1073742080)}

            );
            v_spfunction6 = new Variant[Variant.Create(64)];
            v_spfunction6.InitArray(new Variant[]
            {
            Variant.Create(536870928), Variant.Create(541065216), Variant.Create(16384), Variant.Create(541081616), Variant.Create(541065216), Variant.Create(16), Variant.Create(541081616), Variant.Create(4194304), Variant.Create(536887296), Variant.Create(4210704), Variant.Create(4194304), Variant.Create(536870928), Variant.Create(4194320), Variant.Create(536887296), Variant.Create(536870912), Variant.Create(16400), Variant.Create(0), Variant.Create(4194320), Variant.Create(536887312), Variant.Create(16384), Variant.Create(4210688), Variant.Create(536887312), Variant.Create(16), Variant.Create(541065232), Variant.Create(541065232), Variant.Create(0), Variant.Create(4210704), Variant.Create(541081600), Variant.Create(16400), Variant.Create(4210688), Variant.Create(541081600), Variant.Create(536870912), Variant.Create(536887296), Variant.Create(16), Variant.Create(541065232), Variant.Create(4210688), Variant.Create(541081616), Variant.Create(4194304), Variant.Create(16400), Variant.Create(536870928), Variant.Create(4194304), Variant.Create(536887296), Variant.Create(536870912), Variant.Create(16400), Variant.Create(536870928), Variant.Create(541081616), Variant.Create(4210688), Variant.Create(541065216), Variant.Create(4210704), Variant.Create(541081600), Variant.Create(0), Variant.Create(541065232), Variant.Create(16), Variant.Create(16384), Variant.Create(541065216), Variant.Create(4210704), Variant.Create(16384), Variant.Create(4194320), Variant.Create(536887312), Variant.Create(0), Variant.Create(541081600), Variant.Create(536870912), Variant.Create(4194320), Variant.Create(536887312)}

            );
            v_spfunction7 = new Variant[Variant.Create(64)];
            v_spfunction7.InitArray(new Variant[]
            {
            Variant.Create(2097152), Variant.Create(69206018), Variant.Create(67110914), Variant.Create(0), Variant.Create(2048), Variant.Create(67110914), Variant.Create(2099202), Variant.Create(69208064), Variant.Create(69208066), Variant.Create(2097152), Variant.Create(0), Variant.Create(67108866), Variant.Create(2), Variant.Create(67108864), Variant.Create(69206018), Variant.Create(2050), Variant.Create(67110912), Variant.Create(2099202), Variant.Create(2097154), Variant.Create(67110912), Variant.Create(67108866), Variant.Create(69206016), Variant.Create(69208064), Variant.Create(2097154), Variant.Create(69206016), Variant.Create(2048), Variant.Create(2050), Variant.Create(69208066), Variant.Create(2099200), Variant.Create(2), Variant.Create(67108864), Variant.Create(2099200), Variant.Create(67108864), Variant.Create(2099200), Variant.Create(2097152), Variant.Create(67110914), Variant.Create(67110914), Variant.Create(69206018), Variant.Create(69206018), Variant.Create(2), Variant.Create(2097154), Variant.Create(67108864), Variant.Create(67110912), Variant.Create(2097152), Variant.Create(69208064), Variant.Create(2050), Variant.Create(2099202), Variant.Create(69208064), Variant.Create(2050), Variant.Create(67108866), Variant.Create(69208066), Variant.Create(69206016), Variant.Create(2099200), Variant.Create(0), Variant.Create(2), Variant.Create(69208066), Variant.Create(0), Variant.Create(2099202), Variant.Create(69206016), Variant.Create(2048), Variant.Create(67108866), Variant.Create(67110912), Variant.Create(2048), Variant.Create(2097154)}

            );
            v_spfunction8 = new Variant[Variant.Create(64)];
            v_spfunction8.InitArray(new Variant[]
            {
            Variant.Create(268439616), Variant.Create(4096), Variant.Create(262144), Variant.Create(268701760), Variant.Create(268435456), Variant.Create(268439616), Variant.Create(64), Variant.Create(268435456), Variant.Create(262208), Variant.Create(268697600), Variant.Create(268701760), Variant.Create(266240), Variant.Create(268701696), Variant.Create(266304), Variant.Create(4096), Variant.Create(64), Variant.Create(268697600), Variant.Create(268435520), Variant.Create(268439552), Variant.Create(4160), Variant.Create(266240), Variant.Create(262208), Variant.Create(268697664), Variant.Create(268701696), Variant.Create(4160), Variant.Create(0), Variant.Create(0), Variant.Create(268697664), Variant.Create(268435520), Variant.Create(268439552), Variant.Create(266304), Variant.Create(262144), Variant.Create(266304), Variant.Create(262144), Variant.Create(268701696), Variant.Create(4096), Variant.Create(64), Variant.Create(268697664), Variant.Create(4096), Variant.Create(266304), Variant.Create(268439552), Variant.Create(64), Variant.Create(268435520), Variant.Create(268697600), Variant.Create(268697664), Variant.Create(268435456), Variant.Create(262144), Variant.Create(268439616), Variant.Create(0), Variant.Create(268701760), Variant.Create(262208), Variant.Create(268435520), Variant.Create(268697600), Variant.Create(268439552), Variant.Create(268439616), Variant.Create(0), Variant.Create(268701760), Variant.Create(266240), Variant.Create(266240), Variant.Create(4160), Variant.Create(4160), Variant.Create(262208), Variant.Create(268435456), Variant.Create(268701696)}

            );
            v_masks = new Variant[Variant.Create(33)];
            v_masks.InitArray(new Variant[]
            {
            Variant.Create(4294967295L), Variant.Create(2147483647), Variant.Create(1073741823), Variant.Create(536870911), Variant.Create(268435455), Variant.Create(134217727), Variant.Create(67108863), Variant.Create(33554431), Variant.Create(16777215), Variant.Create(8388607), Variant.Create(4194303), Variant.Create(2097151), Variant.Create(1048575), Variant.Create(524287), Variant.Create(262143), Variant.Create(131071), Variant.Create(65535), Variant.Create(32767), Variant.Create(16383), Variant.Create(8191), Variant.Create(4095), Variant.Create(2047), Variant.Create(1023), Variant.Create(511), Variant.Create(255), Variant.Create(127), Variant.Create(63), Variant.Create(31), Variant.Create(15), Variant.Create(7), Variant.Create(3), Variant.Create(1), Variant.Create(0)}

            );
            v_keys = f_des_createKeys(v_key);
            if (_functions.EqualString(_context.Error, Variant.Create(1)))
            {
                _functions.SetError(Variant.Create(-1));
                return Variant.Create(0);
            }

            v_m = Variant.Create(0);
            v_chunk = Variant.Create(0);
            if (_functions.EqualString(_functions.UBound(v_keys), Variant.Create(32)))
            {
                v_iterations = Variant.Create(3);
            }
            else
            {
                v_iterations = Variant.Create(9);
            }

            if (_functions.EqualString(v_iterations, Variant.Create(3)))
            {
                if (_functions.EqualString(v_encrypt, Variant.Create(1)))
                {
                    v_looping = new Variant[Variant.Create(3)];
                    v_looping.InitArray(new Variant[]
                    {
                    Variant.Create(0), Variant.Create(32), Variant.Create(2)}

                    );
                }
                else
                {
                    v_looping = new Variant[Variant.Create(3)];
                    v_looping.InitArray(new Variant[]
                    {
                    Variant.Create(30), Variant.Create(-2), Variant.Create(-2)}

                    );
                }
            }
            else
            {
                if (_functions.EqualString(v_encrypt, Variant.Create(1)))
                {
                    v_looping = new Variant[Variant.Create(9)];
                    v_looping.InitArray(new Variant[]
                    {
                    Variant.Create(0), Variant.Create(32), Variant.Create(2), Variant.Create(62), Variant.Create(30), Variant.Create(-2), Variant.Create(64), Variant.Create(96), Variant.Create(2)}

                    );
                }
                else
                {
                    v_looping = new Variant[Variant.Create(9)];
                    v_looping.InitArray(new Variant[]
                    {
                    Variant.Create(94), Variant.Create(62), Variant.Create(-2), Variant.Create(32), Variant.Create(64), Variant.Create(2), Variant.Create(30), Variant.Create(-2), Variant.Create(-2)}

                    );
                }
            }

            if (_functions.EqualString(v_encrypt, Variant.Create(1)))
            {
                v_fill = Variant.Create(8);
                if (_functions.Mod(_functions.StringLen(v_message), Variant.Create(8)) != Variant.Create(0))
                {
                    v_fill = Variant.Create(8) - _functions.Mod(_functions.StringLen(v_message), Variant.Create(8));
                }

                loopHandler04edc183cb6f46f5b364670bbfa31989 = new ForToNextLooper(Variant.Create(1), v_fill, null);
                v_i = loopHandler04edc183cb6f46f5b364670bbfa31989.Index;
                for (; loopHandler04edc183cb6f46f5b364670bbfa31989.MoveNext(); v_i = loopHandler04edc183cb6f46f5b364670bbfa31989.Index)
                {
                    v_message = v_message.ConcatAssign(_functions.Chr(v_fill));
                ConinueLoop_level_1_count_0:
                    ;
                }

            ExitLooP_level_1_count_0:
                ;
            }

            v_len = _functions.StringLen(v_message);
            v_result = Variant.Create("");
            v_tempresult = Variant.Create("");
            if (_functions.EqualString(v_mode, Variant.Create(1)))
            {
                v_cbcleft = _functions.BitOR(_functions.BitShift(_functions.Asc(_functions.StringMid(v_iv, Variant.Create(1), Variant.Create(1))), Variant.Create(-24)), _functions.BitShift(_functions.Asc(_functions.StringMid(v_iv, Variant.Create(2), Variant.Create(1))), Variant.Create(-16)), _functions.BitShift(_functions.Asc(_functions.StringMid(v_iv, Variant.Create(3), Variant.Create(1))), Variant.Create(-8)), _functions.Asc(_functions.StringMid(v_iv, Variant.Create(4), Variant.Create(1))));
                v_cbcright = _functions.BitOR(_functions.BitShift(_functions.Asc(_functions.StringMid(v_iv, Variant.Create(5), Variant.Create(1))), Variant.Create(-24)), _functions.BitShift(_functions.Asc(_functions.StringMid(v_iv, Variant.Create(6), Variant.Create(1))), Variant.Create(-16)), _functions.BitShift(_functions.Asc(_functions.StringMid(v_iv, Variant.Create(7), Variant.Create(1))), Variant.Create(-8)), _functions.Asc(_functions.StringMid(v_iv, Variant.Create(8), Variant.Create(1))));
            }

            while (v_m < v_len)
            {
                v_left = _functions.BitOR(_functions.BitShift(_functions.Asc(_functions.StringMid(v_message, v_m + Variant.Create(1), Variant.Create(1))), Variant.Create(-24)), _functions.BitShift(_functions.Asc(_functions.StringMid(v_message, v_m + Variant.Create(2), Variant.Create(1))), Variant.Create(-16)), _functions.BitShift(_functions.Asc(_functions.StringMid(v_message, v_m + Variant.Create(3), Variant.Create(1))), Variant.Create(-8)), _functions.Asc(_functions.StringMid(v_message, v_m + Variant.Create(4), Variant.Create(1))));
                v_right = _functions.BitOR(_functions.BitShift(_functions.Asc(_functions.StringMid(v_message, v_m + Variant.Create(5), Variant.Create(1))), Variant.Create(-24)), _functions.BitShift(_functions.Asc(_functions.StringMid(v_message, v_m + Variant.Create(6), Variant.Create(1))), Variant.Create(-16)), _functions.BitShift(_functions.Asc(_functions.StringMid(v_message, v_m + Variant.Create(7), Variant.Create(1))), Variant.Create(-8)), _functions.Asc(_functions.StringMid(v_message, v_m + Variant.Create(8), Variant.Create(1))));
                v_m += Variant.Create(8);
                if (_functions.EqualString(v_mode, Variant.Create(1)))
                {
                    if (v_encrypt)
                    {
                        v_left = _functions.BitXOR(v_left, v_cbcleft);
                        v_right = _functions.BitXOR(v_right, v_cbcright);
                    }
                    else
                    {
                        v_cbcleft2 = v_cbcleft;
                        v_cbcright2 = v_cbcright;
                        v_cbcleft = v_left;
                        v_cbcright = v_right;
                    }
                }

                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(4)), v_masks[Variant.Create(4)]), v_right), Variant.Create(252645135));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-4)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(16)), v_masks[Variant.Create(16)]), v_right), Variant.Create(65535));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-16)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(2)), v_masks[Variant.Create(2)]), v_left), Variant.Create(858993459));
                v_left = _functions.BitXOR(v_left, v_temp);
                v_right = _functions.BitXOR(v_right, _functions.BitShift(v_temp, Variant.Create(-2)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(8)), v_masks[Variant.Create(8)]), v_left), Variant.Create(16711935));
                v_left = _functions.BitXOR(v_left, v_temp);
                v_right = _functions.BitXOR(v_right, _functions.BitShift(v_temp, Variant.Create(-8)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(1)), v_masks[Variant.Create(1)]), v_right), Variant.Create(1431655765));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-1)));
                v_left = _functions.BitOR(_functions.BitShift(v_left, Variant.Create(-1)), _functions.BitAND(_functions.BitShift(v_left, Variant.Create(31)), v_masks[Variant.Create(31)]));
                v_right = _functions.BitOR(_functions.BitShift(v_right, Variant.Create(-1)), _functions.BitAND(_functions.BitShift(v_right, Variant.Create(31)), v_masks[Variant.Create(31)]));
                loopHandler962c28090bc448ba89a8e068435392cd = new ForToNextLooper(Variant.Create(0), v_iterations - Variant.Create(1), Variant.Create(3));
                v_j = loopHandler962c28090bc448ba89a8e068435392cd.Index;
                for (; loopHandler962c28090bc448ba89a8e068435392cd.MoveNext(); v_j = loopHandler962c28090bc448ba89a8e068435392cd.Index)
                {
                    v_endloop = v_looping[v_j + Variant.Create(1)];
                    v_loopinc = v_looping[v_j + Variant.Create(2)];
                    v_i = v_looping[v_j];
                    while (v_i != v_endloop)
                    {
                        v_right1 = _functions.BitXOR(v_right, v_keys[v_i]);
                        v_right2 = _functions.BitXOR(_functions.BitOR(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(4)), v_masks[Variant.Create(4)]), _functions.BitShift(v_right, Variant.Create(-28))), v_keys[v_i + Variant.Create(1)]);
                        v_temp = v_left;
                        v_left = v_right;
                        v_right = _functions.BitXOR(v_temp, _functions.BitOR(v_spfunction2[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right1, Variant.Create(24)), v_masks[Variant.Create(24)]), Variant.Create(63))], v_spfunction4[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right1, Variant.Create(16)), v_masks[Variant.Create(16)]), Variant.Create(63))], v_spfunction6[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right1, Variant.Create(8)), v_masks[Variant.Create(8)]), Variant.Create(63))], v_spfunction8[_functions.BitAND(v_right1, Variant.Create(63))], v_spfunction1[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right2, Variant.Create(24)), v_masks[Variant.Create(24)]), Variant.Create(63))], v_spfunction3[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right2, Variant.Create(16)), v_masks[Variant.Create(16)]), Variant.Create(63))], v_spfunction5[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right2, Variant.Create(8)), v_masks[Variant.Create(8)]), Variant.Create(63))], v_spfunction7[_functions.BitAND(v_right2, Variant.Create(63))]));
                        v_i += v_loopinc;
                    ConinueLoop_level_3_count_0:
                        ;
                    }

                ExitLooP_level_3_count_0:
                    ;
                    v_temp = v_left;
                    v_left = v_right;
                    v_right = v_temp;
                ConinueLoop_level_2_count_0:
                    ;
                }

            ExitLooP_level_2_count_0:
                ;
                v_left = _functions.BitOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(1)), v_masks[Variant.Create(1)]), _functions.BitShift(v_left, Variant.Create(-31)));
                v_right = _functions.BitOR(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(1)), v_masks[Variant.Create(1)]), _functions.BitShift(v_right, Variant.Create(-31)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(1)), v_masks[Variant.Create(1)]), v_right), Variant.Create(1431655765));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-1)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(8)), v_masks[Variant.Create(8)]), v_left), Variant.Create(16711935));
                v_left = _functions.BitXOR(v_left, v_temp);
                v_right = _functions.BitXOR(v_right, _functions.BitShift(v_temp, Variant.Create(-8)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(2)), v_masks[Variant.Create(2)]), v_left), Variant.Create(858993459));
                v_left = _functions.BitXOR(v_left, v_temp);
                v_right = _functions.BitXOR(v_right, _functions.BitShift(v_temp, Variant.Create(-2)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(16)), v_masks[Variant.Create(16)]), v_right), Variant.Create(65535));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-16)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(4)), v_masks[Variant.Create(4)]), v_right), Variant.Create(252645135));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-4)));
                if (_functions.EqualString(v_mode, Variant.Create(1)))
                {
                    if (_functions.EqualString(v_encrypt, Variant.Create(1)))
                    {
                        v_cbcleft = v_left;
                        v_cbcright = v_right;
                    }
                    else
                    {
                        v_left = _functions.BitXOR(v_left, v_cbcleft2);
                        v_right = _functions.BitXOR(v_right, v_cbcright2);
                    }
                }

                v_tempresult = v_tempresult.ConcatAssign(_functions.Chr(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(24)), v_masks[Variant.Create(24)])));
                v_tempresult = v_tempresult.ConcatAssign(_functions.Chr(_functions.BitAND(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(16)), v_masks[Variant.Create(16)]), Variant.Create(255))));
                v_tempresult = v_tempresult.ConcatAssign(_functions.Chr(_functions.BitAND(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(8)), v_masks[Variant.Create(8)]), Variant.Create(255))));
                v_tempresult = v_tempresult.ConcatAssign(_functions.Chr(_functions.BitAND(v_left, Variant.Create(255))));
                v_tempresult = v_tempresult.ConcatAssign(_functions.Chr(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(24)), v_masks[Variant.Create(24)])));
                v_tempresult = v_tempresult.ConcatAssign(_functions.Chr(_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(16)), v_masks[Variant.Create(16)]), Variant.Create(255))));
                v_tempresult = v_tempresult.ConcatAssign(_functions.Chr(_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(8)), v_masks[Variant.Create(8)]), Variant.Create(255))));
                v_tempresult = v_tempresult.ConcatAssign(_functions.Chr(_functions.BitAND(v_right, Variant.Create(255))));
                v_chunk += Variant.Create(8);
                if (_functions.EqualString(v_chunk, Variant.Create(512)))
                {
                    v_result = v_result.ConcatAssign(v_tempresult);
                    v_tempresult = Variant.Create("");
                    v_chunk = Variant.Create(0);
                }

            ConinueLoop_level_1_count_1:
                ;
            }

        ExitLooP_level_1_count_1:
            ;
            v_result = v_result.ConcatAssign(v_tempresult);
            if (_functions.EqualString(v_encrypt, Variant.Create(0)))
            {
                v_fill = _functions.Asc(_functions.StringRight(v_result, Variant.Create(1)));
                v_result = _functions.StringTrimRight(v_result, v_fill);
            }

            return v_result;
            return Variant.Create((object)null);
        }

        public Variant f_des_createKeys(Variant v_key)
        {
            Variant v_pc2bytes0 = Variant.Create((object)null);
            Variant v_pc2bytes1 = Variant.Create((object)null);
            Variant v_pc2bytes2 = Variant.Create((object)null);
            Variant v_pc2bytes3 = Variant.Create((object)null);
            Variant v_pc2bytes4 = Variant.Create((object)null);
            Variant v_pc2bytes5 = Variant.Create((object)null);
            Variant v_pc2bytes6 = Variant.Create((object)null);
            Variant v_pc2bytes7 = Variant.Create((object)null);
            Variant v_pc2bytes8 = Variant.Create((object)null);
            Variant v_pc2bytes9 = Variant.Create((object)null);
            Variant v_pc2bytes10 = Variant.Create((object)null);
            Variant v_pc2bytes11 = Variant.Create((object)null);
            Variant v_pc2bytes12 = Variant.Create((object)null);
            Variant v_pc2bytes13 = Variant.Create((object)null);
            Variant v_masks = Variant.Create((object)null);
            Variant v_iterations = Variant.Create((object)null);
            Variant v_keys = Variant.Create((object)null);
            Variant v_shifts = Variant.Create((object)null);
            Variant v_lefttemp = Variant.Create((object)null);
            Variant v_righttemp = Variant.Create((object)null);
            Variant v_m = Variant.Create((object)null);
            Variant v_n = Variant.Create((object)null);
            Variant v_temp = Variant.Create((object)null);
            ForToNextLooper loopHandlerdc390a5919b34648beb721c0f67261d8;
            Variant v_j = null;
            Variant v_mid = Variant.Create((object)null);
            Variant v_dec = Variant.Create((object)null);
            Variant v_shift = Variant.Create((object)null);
            Variant v_left = Variant.Create((object)null);
            Variant v_right = Variant.Create((object)null);
            ForToNextLooper loopHandlerd9fcdf17bf8b43f68352b7df515fb227;
            Variant v_i = null;
            v_pc2bytes0 = new Variant[Variant.Create(16)];
            v_pc2bytes0.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(4), Variant.Create(536870912), Variant.Create(536870916), Variant.Create(65536), Variant.Create(65540), Variant.Create(536936448), Variant.Create(536936452), Variant.Create(512), Variant.Create(516), Variant.Create(536871424), Variant.Create(536871428), Variant.Create(66048), Variant.Create(66052), Variant.Create(536936960), Variant.Create(536936964)}

            );
            v_pc2bytes1 = new Variant[Variant.Create(16)];
            v_pc2bytes1.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(1), Variant.Create(1048576), Variant.Create(1048577), Variant.Create(67108864), Variant.Create(67108865), Variant.Create(68157440), Variant.Create(68157441), Variant.Create(256), Variant.Create(257), Variant.Create(1048832), Variant.Create(1048833), Variant.Create(67109120), Variant.Create(67109121), Variant.Create(68157696), Variant.Create(68157697)}

            );
            v_pc2bytes2 = new Variant[Variant.Create(16)];
            v_pc2bytes2.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(8), Variant.Create(2048), Variant.Create(2056), Variant.Create(16777216), Variant.Create(16777224), Variant.Create(16779264), Variant.Create(16779272), Variant.Create(0), Variant.Create(8), Variant.Create(2048), Variant.Create(2056), Variant.Create(16777216), Variant.Create(16777224), Variant.Create(16779264), Variant.Create(16779272)}

            );
            v_pc2bytes3 = new Variant[Variant.Create(16)];
            v_pc2bytes3.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(2097152), Variant.Create(134217728), Variant.Create(136314880), Variant.Create(8192), Variant.Create(2105344), Variant.Create(134225920), Variant.Create(136323072), Variant.Create(131072), Variant.Create(2228224), Variant.Create(134348800), Variant.Create(136445952), Variant.Create(139264), Variant.Create(2236416), Variant.Create(134356992), Variant.Create(136454144)}

            );
            v_pc2bytes4 = new Variant[Variant.Create(16)];
            v_pc2bytes4.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(262144), Variant.Create(16), Variant.Create(262160), Variant.Create(0), Variant.Create(262144), Variant.Create(16), Variant.Create(262160), Variant.Create(4096), Variant.Create(266240), Variant.Create(4112), Variant.Create(266256), Variant.Create(4096), Variant.Create(266240), Variant.Create(4112), Variant.Create(266256)}

            );
            v_pc2bytes5 = new Variant[Variant.Create(16)];
            v_pc2bytes5.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(1024), Variant.Create(32), Variant.Create(1056), Variant.Create(0), Variant.Create(1024), Variant.Create(32), Variant.Create(1056), Variant.Create(33554432), Variant.Create(33555456), Variant.Create(33554464), Variant.Create(33555488), Variant.Create(33554432), Variant.Create(33555456), Variant.Create(33554464), Variant.Create(33555488)}

            );
            v_pc2bytes6 = new Variant[Variant.Create(16)];
            v_pc2bytes6.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(268435456), Variant.Create(524288), Variant.Create(268959744), Variant.Create(2), Variant.Create(268435458), Variant.Create(524290), Variant.Create(268959746), Variant.Create(0), Variant.Create(268435456), Variant.Create(524288), Variant.Create(268959744), Variant.Create(2), Variant.Create(268435458), Variant.Create(524290), Variant.Create(268959746)}

            );
            v_pc2bytes7 = new Variant[Variant.Create(16)];
            v_pc2bytes7.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(65536), Variant.Create(2048), Variant.Create(67584), Variant.Create(536870912), Variant.Create(536936448), Variant.Create(536872960), Variant.Create(536938496), Variant.Create(131072), Variant.Create(196608), Variant.Create(133120), Variant.Create(198656), Variant.Create(537001984), Variant.Create(537067520), Variant.Create(537004032), Variant.Create(537069568)}

            );
            v_pc2bytes8 = new Variant[Variant.Create(16)];
            v_pc2bytes8.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(262144), Variant.Create(0), Variant.Create(262144), Variant.Create(2), Variant.Create(262146), Variant.Create(2), Variant.Create(262146), Variant.Create(33554432), Variant.Create(33816576), Variant.Create(33554432), Variant.Create(33816576), Variant.Create(33554434), Variant.Create(33816578), Variant.Create(33554434), Variant.Create(33816578)}

            );
            v_pc2bytes9 = new Variant[Variant.Create(16)];
            v_pc2bytes9.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(268435456), Variant.Create(8), Variant.Create(268435464), Variant.Create(0), Variant.Create(268435456), Variant.Create(8), Variant.Create(268435464), Variant.Create(1024), Variant.Create(268436480), Variant.Create(1032), Variant.Create(268436488), Variant.Create(1024), Variant.Create(268436480), Variant.Create(1032), Variant.Create(268436488)}

            );
            v_pc2bytes10 = new Variant[Variant.Create(16)];
            v_pc2bytes10.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(32), Variant.Create(0), Variant.Create(32), Variant.Create(1048576), Variant.Create(1048608), Variant.Create(1048576), Variant.Create(1048608), Variant.Create(8192), Variant.Create(8224), Variant.Create(8192), Variant.Create(8224), Variant.Create(1056768), Variant.Create(1056800), Variant.Create(1056768), Variant.Create(1056800)}

            );
            v_pc2bytes11 = new Variant[Variant.Create(16)];
            v_pc2bytes11.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(16777216), Variant.Create(512), Variant.Create(16777728), Variant.Create(2097152), Variant.Create(18874368), Variant.Create(2097664), Variant.Create(18874880), Variant.Create(67108864), Variant.Create(83886080), Variant.Create(67109376), Variant.Create(83886592), Variant.Create(69206016), Variant.Create(85983232), Variant.Create(69206528), Variant.Create(85983744)}

            );
            v_pc2bytes12 = new Variant[Variant.Create(16)];
            v_pc2bytes12.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(4096), Variant.Create(134217728), Variant.Create(134221824), Variant.Create(524288), Variant.Create(528384), Variant.Create(134742016), Variant.Create(134746112), Variant.Create(16), Variant.Create(4112), Variant.Create(134217744), Variant.Create(134221840), Variant.Create(524304), Variant.Create(528400), Variant.Create(134742032), Variant.Create(134746128)}

            );
            v_pc2bytes13 = new Variant[Variant.Create(16)];
            v_pc2bytes13.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(4), Variant.Create(256), Variant.Create(260), Variant.Create(0), Variant.Create(4), Variant.Create(256), Variant.Create(260), Variant.Create(1), Variant.Create(5), Variant.Create(257), Variant.Create(261), Variant.Create(1), Variant.Create(5), Variant.Create(257), Variant.Create(261)}

            );
            v_masks = new Variant[Variant.Create(33)];
            v_masks.InitArray(new Variant[]
            {
            Variant.Create(4294967295L), Variant.Create(2147483647), Variant.Create(1073741823), Variant.Create(536870911), Variant.Create(268435455), Variant.Create(134217727), Variant.Create(67108863), Variant.Create(33554431), Variant.Create(16777215), Variant.Create(8388607), Variant.Create(4194303), Variant.Create(2097151), Variant.Create(1048575), Variant.Create(524287), Variant.Create(262143), Variant.Create(131071), Variant.Create(65535), Variant.Create(32767), Variant.Create(16383), Variant.Create(8191), Variant.Create(4095), Variant.Create(2047), Variant.Create(1023), Variant.Create(511), Variant.Create(255), Variant.Create(127), Variant.Create(63), Variant.Create(31), Variant.Create(15), Variant.Create(7), Variant.Create(3), Variant.Create(1), Variant.Create(0)}

            );
            if (_functions.EqualString(_functions.StringLen(v_key), Variant.Create(48)))
            {
                v_iterations = Variant.Create(3);
            }
            else if (_functions.EqualString(_functions.StringLen(v_key), Variant.Create(16)))
            {
                v_iterations = Variant.Create(1);
            }
            else
            {
                _functions.SetError(Variant.Create(1));
                return Variant.Create(0);
            }

            v_keys = new Variant[Variant.Create(32) * v_iterations];
            v_shifts = new Variant[Variant.Create(16)];
            v_shifts.InitArray(new Variant[]
            {
            Variant.Create(0), Variant.Create(0), Variant.Create(1), Variant.Create(1), Variant.Create(1), Variant.Create(1), Variant.Create(1), Variant.Create(1), Variant.Create(0), Variant.Create(1), Variant.Create(1), Variant.Create(1), Variant.Create(1), Variant.Create(1), Variant.Create(1), Variant.Create(0)}

            );
            v_m = Variant.Create(0);
            v_n = Variant.Create(0);
            loopHandlerdc390a5919b34648beb721c0f67261d8 = new ForToNextLooper(Variant.Create(1), v_iterations, null);
            v_j = loopHandlerdc390a5919b34648beb721c0f67261d8.Index;
            for (; loopHandlerdc390a5919b34648beb721c0f67261d8.MoveNext(); v_j = loopHandlerdc390a5919b34648beb721c0f67261d8.Index)
            {
                v_mid = _functions.StringMid(v_key, v_m + Variant.Create(1), Variant.Create(2));
                v_dec = _functions.Dec(v_mid);
                v_shift = _functions.BitShift(v_dec, Variant.Create(-24));
                v_left = _functions.BitOR(v_shift, _functions.BitShift(_functions.Dec(_functions.StringMid(v_key, v_m + Variant.Create(3), Variant.Create(2))), Variant.Create(-16)), _functions.BitShift(_functions.Dec(_functions.StringMid(v_key, v_m + Variant.Create(5), Variant.Create(2))), Variant.Create(-8)), _functions.Dec(_functions.StringMid(v_key, v_m + Variant.Create(7), Variant.Create(2))));
                v_right = _functions.BitOR(_functions.BitShift(_functions.Dec(_functions.StringMid(v_key, v_m + Variant.Create(9), Variant.Create(2))), Variant.Create(-24)), _functions.BitShift(_functions.Dec(_functions.StringMid(v_key, v_m + Variant.Create(11), Variant.Create(2))), Variant.Create(-16)), _functions.BitShift(_functions.Dec(_functions.StringMid(v_key, v_m + Variant.Create(13), Variant.Create(2))), Variant.Create(-8)), _functions.Dec(_functions.StringMid(v_key, v_m + Variant.Create(15), Variant.Create(2))));
                v_m += Variant.Create(16);
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(4)), v_masks[Variant.Create(4)]), v_right), Variant.Create(252645135));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-4)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(16)), v_masks[Variant.Create(16)]), v_left), Variant.Create(65535));
                v_left = _functions.BitXOR(v_left, v_temp);
                v_right = _functions.BitXOR(v_right, _functions.BitShift(v_temp, Variant.Create(-16)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(2)), v_masks[Variant.Create(2)]), v_right), Variant.Create(858993459));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-2)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(16)), v_masks[Variant.Create(16)]), v_left), Variant.Create(65535));
                v_left = _functions.BitXOR(v_left, v_temp);
                v_right = _functions.BitXOR(v_right, _functions.BitShift(v_temp, Variant.Create(-16)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(1)), v_masks[Variant.Create(1)]), v_right), Variant.Create(1431655765));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-1)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(8)), v_masks[Variant.Create(8)]), v_left), Variant.Create(16711935));
                v_left = _functions.BitXOR(v_left, v_temp);
                v_right = _functions.BitXOR(v_right, _functions.BitShift(v_temp, Variant.Create(-8)));
                v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(1)), v_masks[Variant.Create(1)]), v_right), Variant.Create(1431655765));
                v_right = _functions.BitXOR(v_right, v_temp);
                v_left = _functions.BitXOR(v_left, _functions.BitShift(v_temp, Variant.Create(-1)));
                v_temp = _functions.BitOR(_functions.BitShift(v_left, Variant.Create(-8)), _functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(20)), v_masks[Variant.Create(20)]), Variant.Create(240)));
                v_left = _functions.BitOR(_functions.BitShift(v_right, Variant.Create(-24)), _functions.BitAND(_functions.BitShift(v_right, Variant.Create(-8)), Variant.Create(16711680)), _functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(8)), v_masks[Variant.Create(8)]), Variant.Create(65280)), _functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(24)), v_masks[Variant.Create(24)]), Variant.Create(240)));
                v_right = v_temp;
                loopHandlerd9fcdf17bf8b43f68352b7df515fb227 = new ForToNextLooper(Variant.Create(0), _functions.UBound(v_shifts) - Variant.Create(1), null);
                v_i = loopHandlerd9fcdf17bf8b43f68352b7df515fb227.Index;
                for (; loopHandlerd9fcdf17bf8b43f68352b7df515fb227.MoveNext(); v_i = loopHandlerd9fcdf17bf8b43f68352b7df515fb227.Index)
                {
                    if (_functions.EqualString(v_shifts[v_i], Variant.Create(1)))
                    {
                        v_left = _functions.BitOR(_functions.BitShift(v_left, Variant.Create(-2)), _functions.BitAND(_functions.BitShift(v_left, Variant.Create(26)), v_masks[Variant.Create(26)]));
                        v_right = _functions.BitOR(_functions.BitShift(v_right, Variant.Create(-2)), _functions.BitAND(_functions.BitShift(v_right, Variant.Create(26)), v_masks[Variant.Create(26)]));
                    }
                    else
                    {
                        v_left = _functions.BitOR(_functions.BitShift(v_left, Variant.Create(-1)), _functions.BitAND(_functions.BitShift(v_left, Variant.Create(27)), v_masks[Variant.Create(27)]));
                        v_right = _functions.BitOR(_functions.BitShift(v_right, Variant.Create(-1)), _functions.BitAND(_functions.BitShift(v_right, Variant.Create(27)), v_masks[Variant.Create(27)]));
                    }

                    v_left = _functions.BitAND(v_left, Variant.Create(-15));
                    v_right = _functions.BitAND(v_right, Variant.Create(-15));
                    v_lefttemp = _functions.BitOR(v_pc2bytes0[_functions.BitAND(_functions.BitShift(v_left, Variant.Create(28)), v_masks[Variant.Create(28)])], v_pc2bytes1[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(24)), v_masks[Variant.Create(24)]), Variant.Create(15))], v_pc2bytes2[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(20)), v_masks[Variant.Create(20)]), Variant.Create(15))], v_pc2bytes3[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(16)), v_masks[Variant.Create(16)]), Variant.Create(15))], v_pc2bytes4[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(12)), v_masks[Variant.Create(12)]), Variant.Create(15))], v_pc2bytes5[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(8)), v_masks[Variant.Create(8)]), Variant.Create(15))], v_pc2bytes6[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_left, Variant.Create(4)), v_masks[Variant.Create(4)]), Variant.Create(15))]);
                    v_righttemp = _functions.BitOR(v_pc2bytes7[_functions.BitAND(_functions.BitShift(v_right, Variant.Create(28)), v_masks[Variant.Create(28)])], v_pc2bytes8[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(24)), v_masks[Variant.Create(24)]), Variant.Create(15))], v_pc2bytes9[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(20)), v_masks[Variant.Create(20)]), Variant.Create(15))], v_pc2bytes10[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(16)), v_masks[Variant.Create(16)]), Variant.Create(15))], v_pc2bytes11[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(12)), v_masks[Variant.Create(12)]), Variant.Create(15))], v_pc2bytes12[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(8)), v_masks[Variant.Create(8)]), Variant.Create(15))], v_pc2bytes13[_functions.BitAND(_functions.BitAND(_functions.BitShift(v_right, Variant.Create(4)), v_masks[Variant.Create(4)]), Variant.Create(15))]);
                    v_temp = _functions.BitAND(_functions.BitXOR(_functions.BitAND(_functions.BitShift(v_righttemp, Variant.Create(16)), v_masks[Variant.Create(16)]), v_lefttemp), Variant.Create(65535));
                    v_keys[v_n] = _functions.BitXOR(v_lefttemp, v_temp);
                    v_keys[v_n + Variant.Create(1)] = _functions.BitXOR(v_righttemp, _functions.BitShift(v_temp, Variant.Create(-16)));
                    v_n += Variant.Create(2);
                ConinueLoop_level_2_count_0:
                    ;
                }

            ExitLooP_level_2_count_0:
                ;
            ConinueLoop_level_1_count_0:
                ;
            }

        ExitLooP_level_1_count_0:
            ;
            return v_keys;
            return Variant.Create((object)null);
        }

        public Variant f_stringToHex(Variant v_s)
        {
            Variant v_result = Variant.Create((object)null);
            Variant v_temp = Variant.Create((object)null);
            ForToNextLooper loopHandlere68b0b453bb64e48b2036c150bd79287;
            Variant v_i = null;
            v_result = Variant.Create("0x");
            v_temp = _functions.StringSplit(v_s, Variant.Create(""));
            loopHandlere68b0b453bb64e48b2036c150bd79287 = new ForToNextLooper(Variant.Create(1), v_temp[Variant.Create(0)], null);
            v_i = loopHandlere68b0b453bb64e48b2036c150bd79287.Index;
            for (; loopHandlere68b0b453bb64e48b2036c150bd79287.MoveNext(); v_i = loopHandlere68b0b453bb64e48b2036c150bd79287.Index)
            {
                v_result = v_result.ConcatAssign(_functions.Hex(_functions.Asc(v_temp[v_i]), Variant.Create(2)));
            ConinueLoop_level_1_count_0:
                ;
            }

        ExitLooP_level_1_count_0:
            ;
            return v_result;
            return Variant.Create((object)null);
        }

        public AutoJITScriptClass()
        {
            _context = new AutoitContext<AutoJITScriptClass>(this);
            _functions = new AutoitRuntime<AutoJITScriptClass>(_context);
            Main();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new AutoJITScriptClass();
        }
    }
}