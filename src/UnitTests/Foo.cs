using AutoJITRuntime;

namespace AutoJITScript
{
    public class AutoJITScriptClass
    {
        private AutoitContext<AutoJITScriptClass> _context;
        private AutoitRuntime<AutoJITScriptClass> _functions;
        public Variant Main()
        {
            ForToNextLooper loopHandler94c2019f5a554cab8cc9c33e49103209;
            Variant v_i = null;
            v_timer = _functions.TimerInit();
            loopHandler94c2019f5a554cab8cc9c33e49103209 = new ForToNextLooper(Variant.Create(0), Variant.Create(1337), null);
            v_i = loopHandler94c2019f5a554cab8cc9c33e49103209.Index;
            for (; loopHandler94c2019f5a554cab8cc9c33e49103209.MoveNext(); v_i = loopHandler94c2019f5a554cab8cc9c33e49103209.Index)
            {
                v_encrypted = f__StringEncryptRC4(Variant.Create("hallo liebe omi!"), Variant.Create("acvdelsfkselfslea37834"));
                v_decrypted = f__StringDecryptRC4(v_encrypted, Variant.Create("acvdelsfkselfslea37834"));
            ConinueLoop_level_1_count_0:
                ;
            }

        ExitLooP_level_1_count_0:
            ;
            v_diff = _functions.TimerDiff(v_timer);
            _functions.Exit(v_diff);
            return Variant.Create((object)null);
        }

        Variant v_timer = Variant.Create((object)null);
        Variant v_encrypted = Variant.Create((object)null);
        Variant v_decrypted = Variant.Create((object)null);
        Variant v_diff = Variant.Create((object)null);
        public Variant f__StringEncryptRC4(Variant v_text, Variant v_encryptkey)
        {
            Variant v_sbox = Variant.Create((object)null);
            Variant v_key = Variant.Create((object)null);
            Variant v_temp = Variant.Create((object)null);
            Variant v_a = Variant.Create((object)null);
            Variant v_i = Variant.Create((object)null);
            Variant v_j = Variant.Create((object)null);
            Variant v_k = Variant.Create((object)null);
            Variant v_cipherby = Variant.Create((object)null);
            Variant v_cipher = Variant.Create((object)null);
            ForToNextLooper loopHandlerdf2927124fb54fa9959e74724558828c;
            v_sbox = new Variant[Variant.Create(256)];
            v_key = new Variant[Variant.Create(256)];
            v_i = Variant.Create(0);
            v_j = Variant.Create(0);
            f___RC4Initialize(v_encryptkey, ref v_key, ref v_sbox);
            loopHandlerdf2927124fb54fa9959e74724558828c = new ForToNextLooper(Variant.Create(1), _functions.StringLen(v_text), null);
            v_a = loopHandlerdf2927124fb54fa9959e74724558828c.Index;
            for (; loopHandlerdf2927124fb54fa9959e74724558828c.MoveNext(); v_a = loopHandlerdf2927124fb54fa9959e74724558828c.Index)
            {
                v_i = _functions.Mod(v_i + Variant.Create(1), Variant.Create(256));
                v_j = _functions.Mod(v_j + v_sbox[v_i], Variant.Create(256));
                v_temp = v_sbox[v_i];
                v_sbox[v_i] = v_sbox[v_j];
                v_sbox[v_j] = v_temp;
                v_k = v_sbox[_functions.Mod(v_sbox[v_i] + v_sbox[v_j], Variant.Create(256))];
                v_cipherby = _functions.BitXOR(_functions.Asc(_functions.StringMid(v_text, v_a, Variant.Create(1))), v_k);
                v_cipher = _functions.Concat(v_cipher, _functions.Chr(v_cipherby));
            ConinueLoop_level_1_count_0:
                ;
            }

        ExitLooP_level_1_count_0:
            ;
            return f__StringToHexEx(v_cipher);
            return Variant.Create((object)null);
        }

        public Variant f__StringDecryptRC4(Variant v_text, Variant v_encryptkey)
        {
            Variant v_sbox = Variant.Create((object)null);
            Variant v_key = Variant.Create((object)null);
            Variant v_temp = Variant.Create((object)null);
            Variant v_a = Variant.Create((object)null);
            Variant v_i = Variant.Create((object)null);
            Variant v_j = Variant.Create((object)null);
            Variant v_k = Variant.Create((object)null);
            Variant v_cipherby = Variant.Create((object)null);
            Variant v_cipher = Variant.Create((object)null);
            ForToNextLooper loopHandlercbb8a11ad4024972ac2988c391d63d8d;
            v_sbox = new Variant[Variant.Create(256)];
            v_key = new Variant[Variant.Create(256)];
            v_text = f__HexToStringEx(v_text);
            v_i = Variant.Create(0);
            v_j = Variant.Create(0);
            f___RC4Initialize(v_encryptkey, ref v_key, ref v_sbox);
            loopHandlercbb8a11ad4024972ac2988c391d63d8d = new ForToNextLooper(Variant.Create(1), _functions.StringLen(v_text), null);
            v_a = loopHandlercbb8a11ad4024972ac2988c391d63d8d.Index;
            for (; loopHandlercbb8a11ad4024972ac2988c391d63d8d.MoveNext(); v_a = loopHandlercbb8a11ad4024972ac2988c391d63d8d.Index)
            {
                v_i = _functions.Mod(v_i + Variant.Create(1), Variant.Create(256));
                v_j = _functions.Mod(v_j + v_sbox[v_i], Variant.Create(256));
                v_temp = v_sbox[v_i];
                v_sbox[v_i] = v_sbox[v_j];
                v_sbox[v_j] = v_temp;
                v_k = v_sbox[_functions.Mod(v_sbox[v_i] + v_sbox[v_j], Variant.Create(256))];
                v_cipherby = _functions.BitXOR(_functions.Asc(_functions.StringMid(v_text, v_a, Variant.Create(1))), v_k);
                v_cipher = _functions.Concat(v_cipher, _functions.Chr(v_cipherby));
            ConinueLoop_level_1_count_0:
                ;
            }

        ExitLooP_level_1_count_0:
            ;
            return v_cipher;
            return Variant.Create((object)null);
        }

        public Variant f___RC4Initialize(Variant v_strPwd, ref Variant v_key, ref Variant v_sbox)
        {
            Variant v_tempSwap = Variant.Create((object)null);
            Variant v_a = Variant.Create((object)null);
            Variant v_b = Variant.Create((object)null);
            Variant v_intLength = Variant.Create((object)null);
            ForToNextLooper loopHandler43b446b769014a28bc9d1bd91be599ef;
            ForToNextLooper loopHandlere9c8a94d0fff45fba03d2509ed882c25;
            v_intLength = _functions.StringLen(v_strPwd);
            loopHandler43b446b769014a28bc9d1bd91be599ef = new ForToNextLooper(Variant.Create(0), Variant.Create(255), null);
            v_a = loopHandler43b446b769014a28bc9d1bd91be599ef.Index;
            for (; loopHandler43b446b769014a28bc9d1bd91be599ef.MoveNext(); v_a = loopHandler43b446b769014a28bc9d1bd91be599ef.Index)
            {
                v_key[v_a] = _functions.Asc(_functions.StringMid(v_strPwd, _functions.Mod(v_a, v_intLength) + Variant.Create(1), Variant.Create(1)));
                v_sbox[v_a] = v_a;
            ConinueLoop_level_1_count_0:
                ;
            }

        ExitLooP_level_1_count_0:
            ;
            v_b = Variant.Create(0);
            loopHandlere9c8a94d0fff45fba03d2509ed882c25 = new ForToNextLooper(Variant.Create(0), Variant.Create(255), null);
            v_a = loopHandlere9c8a94d0fff45fba03d2509ed882c25.Index;
            for (; loopHandlere9c8a94d0fff45fba03d2509ed882c25.MoveNext(); v_a = loopHandlere9c8a94d0fff45fba03d2509ed882c25.Index)
            {
                v_b = _functions.Mod(v_b + v_sbox[v_a] + v_key[v_a], Variant.Create(256));
                v_tempSwap = v_sbox[v_a];
                v_sbox[v_a] = v_sbox[v_b];
                v_sbox[v_b] = v_tempSwap;
            ConinueLoop_level_1_count_1:
                ;
            }

        ExitLooP_level_1_count_1:
            ;
            return Variant.Create((object)null);
        }

        public Variant f__HexToStringEx(Variant v_strHex)
        {
            return _functions.BinaryToString(_functions.Concat(Variant.Create("0x"), v_strHex));
            return Variant.Create((object)null);
        }

        public Variant f__StringToHexEx(Variant v_strChar)
        {
            return _functions.Hex(_functions.StringToBinary(v_strChar));
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