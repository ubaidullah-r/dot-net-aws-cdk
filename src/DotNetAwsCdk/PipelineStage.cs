using Amazon.CDK;
using Amazon.CDK.Pipelines;
using Constructs;

namespace CdkWorkshop
{
    public class WorkshopPipelineStage : Stage
    {
        public WorkshopPipelineStage(Construct scope, string id, StageProps props = null)
            : base(scope, id, props)
        {
            var service = new  DotNetAwsCdkStack(this, "WebService");
        }
    }
}