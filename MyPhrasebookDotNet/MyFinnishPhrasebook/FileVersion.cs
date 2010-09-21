using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFinnishPhrasebookNamespace
{
	public class FileVersion
	{
		// NOTE: This string is updated by an external perl script,
		// which runs during a pre-build step of a release version.
		// See: Project settings -> Build Events -> Pre Build Event
		const string commonVersion = "1.2010.0419.2";

		public const string AssemblyVersion = commonVersion;
		public const string AssemblyFileVersion = commonVersion;
	}
}
