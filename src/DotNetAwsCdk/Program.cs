using Amazon.CDK;
using CdkWorkshop;
using Amazon.CDK.AWS.CodeCommit;

namespace DotNetAwsCdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();

            new WorkshopPipelineStack(app, "WorkshopPipelineStack");



            app.Synth();
        }
    }
}
