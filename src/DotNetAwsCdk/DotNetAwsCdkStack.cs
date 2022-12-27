using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Cdklabs.DynamoTableViewer;
using Constructs;

namespace CdkWorkshop
{
    public class DotNetAwsCdkStack : Stack
    {
        public readonly CfnOutput HCViewerUrl;
        public readonly CfnOutput HCEndpoint;
        public DotNetAwsCdkStack(Construct scope, string id) : base(scope, id)
        {
            // Defines a new lambda resource
            var hello = new Function(this, "HelloHandler", new FunctionProps
            {
                Runtime = Runtime.NODEJS_14_X, // execution environment
                Code = Code.FromAsset("lambda"), // Code loaded from the "lambda" directory
                Handler = "hello.handler" // file is "hello", function is "handler"
            });

            // Defines out HitCounter resource
            var helloWithCounter = new HitCounter(this, "HelloHitCounter", new HitCounterProps
            {
                Downstream = hello
            });


            // Defines an API Gateway REST API resource backed by our "hello" function.
            var gateway = new LambdaRestApi(this, "Endpoint", new LambdaRestApiProps
            {
                Handler = helloWithCounter.Handler
            });

            // Defines a new TableViewer resource
            var tv = new TableViewer(this, "ViewerHitCount", new TableViewerProps
            {
                Title = "Hello Hits",
                Table = helloWithCounter.MyTable,
                SortBy = "-hits"
            });
            this.HCViewerUrl = new CfnOutput(this, "TableViewerUrl", new CfnOutputProps
            {
                Value = gateway.Url
            });

            this.HCEndpoint = new CfnOutput(this, "GatewayUrl", new CfnOutputProps
            {
                Value = tv.Endpoint
            });
        }
    }
}