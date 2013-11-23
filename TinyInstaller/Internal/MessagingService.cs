using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TinyInstaller.Internal
{
	static class Messaging
	{
		static readonly MessagingService _service = new MboxMessagingService();

		public static void Message(string caption, string message)
		{
			_service.Message(caption, message);
		}
	}

	abstract class MessagingService
	{
		public abstract void Message(string caption, string message);
	}

	class MboxMessagingService : MessagingService
	{
		public override void Message(string caption, string message)
		{
			MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}


}
