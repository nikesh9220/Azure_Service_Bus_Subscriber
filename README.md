Working with Azure Service Bus Topic and Subscribe.
This document will help you to configure Azure Service Bus Topic and Subscribe. Please
follow below steps to Configure Service Bus namespace, Topic and, Subscribe.
Step 1: Login into https://www.portal.azure.com with your account which have valid
subscription.
Step 2: Search for service bus and Click on Add. Now configure your service bus namespace.
We are using “Topic” and “Subscribe” which will not supported by Basic Price Tier so select
Standard or Premium.
Step 3: Click on your newly created namespace under Service Bus section. And click on Topic
to add new Topic.
Step 4: Create new topic and give appropriate name.
Step 5: Now Click on newly created Topic. You will see below screen now click on new
Subscription.
Step 6: Now give appropriate name and Max delivery count. (It will try for that much time to
resend message.)
Step 7: Now go to your service bus namespace section and click on Shared Access Policy ➔
RootManagerSharedAccessKey to find your connection string and other settings.
Now we have completed Azure Service Bus configuration now it’s time to start
implementing our application to use this.
As you are aware about creating MVC application I’m skipping that steps and jumping into
Publisher Service Section.
Step 1: Install Microsoft.Azure.ServiceBus Package. We will use it to connect with our Topic.
Step 2: Configure Topic Client. You can find your connection string from your service bus
namespace.
Step 3: Our Publisher Service is a wrapper around the TopicClient class of
Microsoft.Azure.ServiceBus Package. The client accepts messages asynchronously, which
must contain a byte array as the message body. We're going to pass a class implementation
as opposed to just a simple string, so we'll need to serialize the data first before converting
it to a UTF8 byte array.
Step 4: we're going to invoke the SendAsync method on our TopicClient instance and pass
our Message to the queue.
Now we have Published our message it’s time to subscribe it and use our message and
perform further processing.
Step 1: Our "Subscriber Service" depends on the Azure Service Bus Nuget Subscription
Client Class.
Step 2: I have configured Max Concurrent Calls to one for now you can increase it. the
cancellation token is passed as a parameter to the callback method in order to determine if
our SubscriptionClient has already been closed. We also need to desacralize our message.
Our data service will handle the part of inserting data into SQL.
I have attached SQL Scripts which will create table, Stored procedure to handle insert
operation. 