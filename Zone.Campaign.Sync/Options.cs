﻿using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace Zone.Campaign.Sync
{
    public class Options
    {
        [ParserState]
        public IParserState LastParserState { get; set; }

        [Option('m', "mode", Required = true,  HelpText ="Mode. [upload, download]")]
        public string Mode { get; set; }

        #region Shared parameters

        [Option('s', "server", Required = true, HelpText = "Server root url, eg. https://neolane.com/.")]
        public string Server { get; set; }

        [Option('u', "username", Required = true, HelpText = "Server username.")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "Server password.")]
        public string Password { get; set; }

        [Option("prompt", HelpText = "Prompt to exit.")]
        public bool PromptToExit { get; set; }

        #endregion

        #region Download parameters

        [Option("dir", HelpText = "Download: Root directory.")]
        public string OutputDirectory { get; set; }

        [Option("dirmode", DefaultValue = "default", HelpText = "Download: Directory mode - option to split on underscore. [default, underscore]")]
        public string DirectoryMode { get; set; }

        [Option("schema", HelpText = "Download: Schema of items to download eg. xtk:jst.")]
        public string Schema { get; set; }

        [OptionList("conditions", HelpText = @"Download: Filter conditions to be applied, eg. ""@namespace = 'zne'"".")]
        public IList<string> Conditions { get; set; }

        #endregion

        #region Upload parameters

        [OptionList("files", HelpText = "Upload: List of filepaths or patterns of items to upload.")]
        public IList<string> UploadFilePaths { get; set; }

        [Option("uploadtest", HelpText = "Upload: Test mode - don't upload, but print list of files.")]
        public bool UploadTestMode { get; set; }

        #endregion

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}