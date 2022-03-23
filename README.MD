# IsInRx

A web app that tells you what planets are in retrograde motion at a given date. It also tells you where it is, how long it will last and when it will stop. Useful for looking up retrograde Mercury in advance.

You can deploy this on Kubernetes by running:

```
kubectl apply -k deploy/kustomize
```

You'll need to have ingress-nginx installed on your cluster, a secret that stores the DB connection string, as well as have the database, which is currently private.

During development, you can use Docker Compose and Visual Studio.

## TODO

* Verify the data. (Jupiter seems to be a bit off - 12.09.2022.)
