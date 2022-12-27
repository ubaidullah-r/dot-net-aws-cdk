using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Cdklabs.DynamoTableViewer;
using CdkWorkshop;
using Constructs;


namespace DotNetAwsCdk
{
    public class DotNetAwsCdkStack : Stack
    {
        internal DotNetAwsCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var hello = new Function(this, "HelloHandler", new FunctionProps
            {
                Runtime = Runtime.NODEJS_14_X,
                Code = Code.FromAsset("lambda"),
                Handler = "hello.handler"


            });
            var helloWithCounter = new HitCounter(this, "HelloHitCounter", new HitCounterProps
            {
                Downstream = hello
            });


            new LambdaRestApi(this, "Endpoint", new LambdaRestApiProps
            {

                Handler = helloWithCounter.Handler


            });
            // Defines a new TableViewer resource
            new TableViewer(this, "ViewerHitCount", new TableViewerProps
            {
                Title = "Hello Hits",
                Table = helloWithCounter.MyTable,
                SortBy = "-hits"      // optional ("-" denotes descending order)



            });


        }
    }
}
