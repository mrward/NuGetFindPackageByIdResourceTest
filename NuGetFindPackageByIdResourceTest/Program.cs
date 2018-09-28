//
// Program.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Configuration;
using NuGet.Packaging.Core;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.VisualStudio;
using NuGet.Versioning;

namespace NuGetFindPackageByIdResourceTest
{
	class MainClass
	{
		public static int Main (string[] args)
		{
			try {
				int threadCount = 125; //MonoDevelop
				ThreadPool.SetMaxThreads (threadCount, threadCount);
				Task.Run (() => {
					Run ();
				});
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
			Console.ReadKey ();
			return 0;
		}

		static void Run ()
		{
			var context = new SourceCacheContext ();
			context.MaxAge = DateTime.UtcNow;

			var token = CancellationToken.None;
			var logger = new CustomLogger ();
			var repositories = GetSourceRepositories ().ToArray ();

			var tasks = new List<Task> ();
			foreach (var id in packageIds) {
				var package = new PackageIdentity (id, new NuGetVersion ("1.0"));

				foreach (var repository in repositories) {
					var resource = repository.GetResource<FindPackageByIdResource> ();
					//var task = Task.Run (() => Run (package, context, resource, logger, token));
					var task = Run (package, context, resource, logger, token);
					tasks.Add (task);
				}
			}

			Task.WhenAll (tasks).Wait ();
			Console.WriteLine ("------DONE------");
		}

		//static Task Run (PackageIdentity package, SourceCacheContext context, FindPackageByIdResource resource, CustomLogger logger, CancellationToken token)
		//{
		//	return resource.GetDependencyInfoAsync (package.Id, package.Version, context, logger, token);
		//}

		static Task Run (PackageIdentity package, SourceCacheContext context, FindPackageByIdResource resource, CustomLogger logger, CancellationToken token)
		{
			return resource.GetAllVersionsAsync (package.Id, context, logger, token);
		}

		static IEnumerable<SourceRepository> GetSourceRepositories ()
		{
			string directory = Path.GetDirectoryName (typeof (MainClass).Assembly.Location);
			var settings = Settings.LoadDefaultSettings (directory, null, null);
			var provider = new SourceRepositoryProvider (settings, Repository.Provider.GetVisualStudio ());
			return provider.GetRepositories ();
		}


		static string[] packageIds = new string[] {
			"microsoft.aspnetcore.hosting",
			"microsoft.extensions.dependencyinjection.abstractions",
			"microsoft.extensions.localization.abstractions",
			"microsoft.aspnetcore.routing",
			"microsoft.aspnetcore.staticfiles",
			"microsoft.aspnetcore.razor.runtime",
			"microsoft.aspnetcore.hosting.abstractions",
			"microsoft.extensions.configuration.abstractions",
			"microsoft.extensions.fileproviders.composite",
			"microsoft.extensions.fileproviders.embedded",
			"microsoft.extensions.fileproviders.physical",
			"microsoft.aspnetcore.http",
			"microsoft.extensions.configuration",
			"microsoft.extensions.dependencyinjection",
			"microsoft.extensions.logging.abstractions",
			"newtonsoft.json",
			"microsoft.extensions.fileproviders.abstractions",
			"nlog.web.aspnetcore",
			"microsoft.aspnetcore.http.abstractions",
			"microsoft.extensions.caching.abstractions",
			"microsoft.extensions.caching.memory",
			"microsoft.extensions.primitives",
			"yessql.core",
			"microsoft.extensions.configuration.json",
			"microsoft.extensions.configuration.xml",
			"microsoft.extensions.options",
			"microsoft.extensions.configuration.fileextensions",
			"yamldotnet",
			"microsoft.aspnetcore.authentication",
			"microsoft.aspnetcore.antiforgery",
			"microsoft.extensions.localization",
			"microsoft.extensions.logging",
			"nodatime",
			"microsoft.aspnetcore.mvc",
			"microsoft.aspnetcore.mvc.viewfeatures",
			"microsoft.aspnetcore.authorization",
			"microsoft.aspnetcore.dataprotection.extension",
			"fluid.core",
			"microsoft.aspnetcore.routing.abstractions",
			"microsoft.aspnetcore.mvc.dataannotation",
			"microsoft.aspnetcore.dataprotection.azurestorage",
			"microsoft.extensions.configuration.binder",
			"microsoft.aspnetcore.diagnostics",
			"microsoft.extensions.http.polly",
			"microsoft.aspnetcore.httpspolicy",
			"microsoft.aspnetcore.rewrite",
			"yessql.provider.sqlserver",
			"yessql.provider.postgresql",
			"yessql.provider.sqlite",
			"lucene.net.analysis.common",
			"lucene.net.queryparser",
			"lucene.net",
			"markdig",
			"microsoft.aspnetcore.mvc.taghelpers",
			"sixlabors.imagesharp.web",
			"windowsazure.storage",
			"miniprofiler.aspnetcore.mvc"
		};
	}
}
