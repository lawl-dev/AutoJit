using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AutoJIT.Compiler;
using AutoJIT.CSharpConverter.ConversionModule;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Parser.Interface;
using AutoJIT.Parser.Optimizer;
using Microsoft.CodeAnalysis;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private readonly IScriptParser _scriptParser;
        private readonly IAutoitToCSharpConverter _autoitToCSharpConverter;
        private readonly IOptimizer _optimizer;
        private ICompiler _instance;

        public Form1() {
            InitializeComponent();
            var standardAutoJITContainer = new CompilerBootStrapper();
            _scriptParser = standardAutoJITContainer.GetInstance<IScriptParser>();
            _optimizer = standardAutoJITContainer.GetInstance<IOptimizer>();
            _autoitToCSharpConverter = standardAutoJITContainer.GetInstance<IAutoitToCSharpConverter>();
            _instance = standardAutoJITContainer.GetInstance<ICompiler>();
            textBox1.KeyDown += OnKeyDown;
        }

        private void OnKeyDown( object sender, KeyEventArgs keyEventArgs ) {
            if ( keyEventArgs.KeyCode == Keys.F5 ) {
                var compile = _instance.Compile(
                    ( (TextBox) sender ).Text, OutputKind.ConsoleApplication, true);
                var path = Path.GetTempPath() + "/" + Guid.NewGuid().ToString("N");
                File.WriteAllBytes( path, compile );
                Process.Start( path );
            }
        }

        private void OnChange1( object sender, EventArgs e ) {
            var text = ( (TextBox) sender ).Text;
            try {
                var autoitScriptRootNode = _scriptParser.ParseScript( text, new PragmaOptions() );

                textBox2.Text = _optimizer.Optimize( _autoitToCSharpConverter.Convert( autoitScriptRootNode ).NormalizeWhitespace() ).ToFullString();
            }
            catch (Exception ex) {}
        }

        private void Form1_Load( object sender, EventArgs e ) {}
    }
}
