using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Drawing;
using System.Web;


namespace WpfApp1.exia.ipc.ihm;

public class MessageConsole
{
   private TextBox _textComponent;
   private bool _isAppend;

   public MessageConsole(TextBox textComponent) : this(textComponent, true)
   {
   }

   public MessageConsole(TextBox textComponent, bool isAppend)
   {
       this._textComponent = textComponent;
       this._isAppend = isAppend;
       textComponent.Redo();
   }

   public void RedirectOut()
   {
       this.RedirectOut(new Color(), null);
   }

   public void RedirectOut(Color textColor, TextWriter printStream)
   {
       ConsoleOutputStream cos = new ConsoleOutputStream(textColor, printStream);
       Console.SetOut(new StreamWriter(cos));
   }

   public void RedirectErr()
   {
       this.RedirectErr(new Color(), null);
   }

   public void RedirectErr(Color textColor, TextWriter printStream)
   {
       ConsoleOutputStream cos = new ConsoleOutputStream(textColor, printStream);
       Console.SetError(new StreamWriter(cos));
   }

   class ConsoleOutputStream : MemoryStream
   {
       private string EOL = Environment.NewLine;
       private TextWriter printStream;
       private StringBuilder buffer = new StringBuilder(80);
       private bool isFirstLine;
       private MessageConsole messageConsole;

       public ConsoleOutputStream(Color textColor, TextWriter printStream)
       {
           this.printStream = printStream;
           if (messageConsole._isAppend)
           {
               this.isFirstLine = true;
           }
       }

       public override void Write(byte[] buffer, int offset, int count)
       {
           string message = Encoding.UTF8.GetString(buffer, offset, count);
           
           if (message.Length != 0)
           {
               if (messageConsole._isAppend)
               {
                  this.HandleAppend(message);
               }
               else
               {
                  this.HandleInsert(message);
               }

               this.buffer.Clear();
           }
       }

       private void HandleAppend(string message)
       {
           if (messageConsole._textComponent.MaxLength == 0)
           {
               this.buffer.Clear();
           }

           if (this.EOL.Equals(message))
           {
               this.buffer.Append(message);
           }
           else
           {
               this.buffer.Append(message);
               this.ClearBuffer();
           }
       }

       private void HandleInsert(string message)
       {
           this.buffer.Append(message);
           if (this.EOL.Equals(message))
           {
               this.ClearBuffer();
           }
       }

       private void ClearBuffer()
       {
           
           if (this.isFirstLine && messageConsole._textComponent.MaxLength != 0)
           {
               this.buffer.Insert(0, "\n");
           }

           this.isFirstLine = false;
           string line = this.buffer.ToString();

           messageConsole._textComponent.AppendText(line);
           if (this.printStream != null)
           {
               this.printStream.Write(line);
           }

           this.buffer.Clear();
       }
   }
}
