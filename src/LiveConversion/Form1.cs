using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AutoJIT.Compiler;
using AutoJIT.CSharpConverter.ConversionModule;
using AutoJIT.CSharpConverter.ConversionModule.Optimizer;
using AutoJIT.Parser.AST;
using AutoJIT.Parser.AST.Parser.Interface;
using Microsoft.CodeAnalysis;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private readonly IAutoitToCSharpConverter _autoitToCSharpConverter;
        private readonly ICompiler _instance;
        private readonly IOptimizer _optimizer;
        private readonly IScriptParser _scriptParser;

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
                byte[] compile = _instance.Compile( ( (TextBox) sender ).Text, OutputKind.ConsoleApplication, true );
                string path = Path.GetTempPath()+"/"+Guid.NewGuid().ToString( "N" );
                File.WriteAllBytes( path, compile );
                Process.Start( path );
            }
        }

        private void OnChange1( object sender, EventArgs e ) {
            string text = ( (TextBox) sender ).Text;
            try {
                AutoitScriptRoot autoitScriptRoot = _scriptParser.ParseScript( text, new PragmaOptions() );

                textBox2.Text = _optimizer.Optimize( _autoitToCSharpConverter.Convert( autoitScriptRoot ).NormalizeWhitespace() ).ToFullString();
            }
            catch (Exception ex) {}
        }

        private void Form1_Load( object sender, EventArgs e ) {}
    }
}
