# ScopedSingletonTransient
These are three different lifetime options of the Dependency Injection technique that comes with ASP .NET Core.
## Scoped
It creates an instance in the first request created and that instance is used until a new request arrives. For example, it created an instance while creating the page you are viewing. Then you refreshed that page. Now creates a new instance.
## Transient
I want to start with a common phrase for transient services, "If in doubt, make it transient". Transient services create a new instance on each request. In this way, they are the least binding service. It is almost similar to creating an instance of the same type using "new" keyword and using it.
## Singleton
An instance is created as soon as the application is launched and the same instance is used until the application is closed. One can think of a Singleton service as a kind of "static" types which are instantiated only once and are reused for all times. In web terms, it means that after the initial request of the service, every subsequent request will use the same instance.
