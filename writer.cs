using System;
using System.IO;
using System.Text;

namespace osusb1 {
partial class all {
	class Writer {
		public StreamWriter w;

		public int byteswritten;
		bool comments;

		public Writer(StreamWriter w, bool comments) {
			this.w = w;
			this.comments = comments;
		}

		public virtual void ln(string line) {
			byteswritten += line.Length + 1;
			w.Write(line + "\n");
		}

		public void comment(string line) {
			if (comments) {
				ln("// " + line);
			}
		}
	}
	class CountingWriter : Writer
	{
		public CountingWriter()
			: base(null, false)
		{
		}

		public override void ln(string line)
		{
			this.byteswritten += line.Length + 1;
		}
	}
}
}
