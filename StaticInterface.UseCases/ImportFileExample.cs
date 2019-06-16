using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StaticInterface.UseCases
{
    [TestClass]
    public class ImportFileExample
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mockDirectory = new[] { "file 1 content", "file 2 content", "file 3 content" };
            List<ImportFileBase> result = new List<ImportFileBase>();
            foreach (var fileContent in mockDirectory)
            {
                switch (int.Parse(fileContent[5].ToString()))
                {
                    case 1:
                        result.Add(IntermediateFactory<BillingImportFile>(fileContent));
                        break;
                    case 2:
                        result.Add(IntermediateFactory<GoodsImportFile>(fileContent));
                        break;
                    case 3:
                        result.Add(IntermediateFactory<BillingAndGoodsImportFile>(fileContent));
                        break;
                    default:
                        throw new InvalidOperationException();
                }

            }
        }

        private TImportFile IntermediateFactory<TImportFile>(string contents)
            where TImportFile : ImportFileBase, IConstructible<ImportFileInitializationParams>, new()
        {
            var parameters = new ImportFileInitializationParams(contents);
            return StaticInterfaceFactory.Create<TImportFile, ImportFileInitializationParams>(parameters);
        }

        private class ImportFileBase
        {
            protected string fileContent;
        }

        private class BillingImportFile : ImportFileBase, IConstructible<ImportFileInitializationParams>
        {
            private ImportFileInitializationParams _parameters;

            public void Initialize(ImportFileInitializationParams parameters)
            {
                _parameters = parameters;
                fileContent = parameters.FileContent;
            }
        }

        private class GoodsImportFile : ImportFileBase, IConstructible<ImportFileInitializationParams>
        {
            private ImportFileInitializationParams _parameters;

            public void Initialize(ImportFileInitializationParams parameters)
            {
                _parameters = parameters;
                fileContent = parameters.FileContent;
            }
        }

        private class BillingAndGoodsImportFile : ImportFileBase, IConstructible<ImportFileInitializationParams>
        {
            private ImportFileInitializationParams _parameters;

            public void Initialize(ImportFileInitializationParams parameters)
            {
                _parameters = parameters;
                fileContent = parameters.FileContent;
            }
        }

        private class ImportFileInitializationParams
        {
            public ImportFileInitializationParams(string fileContent)
            {
                FileContent = fileContent;
            }

            public string FileContent { get; private set; }
        }
    }
}
