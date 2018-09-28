//
// CustomLogger.cs
//
// Author:
//       Matt Ward <matt.ward@microsoft.com>
//
// Copyright (c) 2018 Microsoft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Threading.Tasks;
using NuGet.Common;

namespace NuGetFindPackageByIdResourceTest
{
	public class CustomLogger : ILogger
	{
		public CustomLogger ()
		{
		}

		public void Log (LogLevel level, string data)
		{
			Console.WriteLine (data);
		}

		public void Log (ILogMessage message)
		{
			Console.WriteLine (message.Message);
		}

		public Task LogAsync (LogLevel level, string data)
		{
			Console.WriteLine (data);
			return Task.CompletedTask;
		}

		public Task LogAsync (ILogMessage message)
		{
			Console.WriteLine (message.Message);
			return Task.CompletedTask;
		}

		public void LogDebug (string data)
		{
			Console.WriteLine (data);
		}

		public void LogError (string data)
		{
			Console.WriteLine (data);
		}

		public void LogInformation (string data)
		{
			Console.WriteLine (data);
		}

		public void LogInformationSummary (string data)
		{
			Console.WriteLine (data);
		}

		public void LogMinimal (string data)
		{
			Console.WriteLine (data);
		}

		public void LogVerbose (string data)
		{
			Console.WriteLine (data);
		}

		public void LogWarning (string data)
		{
			Console.WriteLine (data);
		}
	}
}
