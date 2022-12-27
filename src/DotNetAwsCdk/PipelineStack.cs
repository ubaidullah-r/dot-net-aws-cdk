using Amazon.CDK;
using Amazon.CDK.AWS.CodeCommit;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.Pipelines;
using Constructs;
namespace CdkWorkshop
{
    public class WorkshopPipelineStack : Stack
    {
        public WorkshopPipelineStack(Construct parent, string id, IStackProps props = null) : base(parent, id, props)
        {
            // Creates a CodeCommit repository called 'WorkshopRepo'
            var repo = new Repository(this, "WorkshopRepo", new RepositoryProps
            {
                RepositoryName = "WorkshopRepo"
            });
            // Pipeline code goes here
            var pipeline = new CodePipeline(this, "Pipeline", new CodePipelineProps
            {
                PipelineName = "WorkshopPipeline",

                // Builds our source code outlined above into a could assembly artifact
                Synth = new ShellStep("Synth", new ShellStepProps
                {
                    Input = CodePipelineSource.CodeCommit(repo, "main"),  // Where to get source code to build
                    Commands = new string[] {
                        "npm install -g aws-cdk",
                        "wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb",
                        "sudo dpkg -i packages-microsoft-prod.deb",
                        "sudo apt-get update",
                        "sudo apt-get install -y dotnet-sdk-3.1",
                        "cd src",
                        "cd ..",
                        "dotnet build"  // Language-specific build cmd
                    }
                }),
            });
        }
    }
}