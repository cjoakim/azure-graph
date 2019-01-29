# Azure Graph API - with Graph Explorer UI in browser

## Links

- https://developer.microsoft.com/en-us/graph/graph-explorer
- https://developer.microsoft.com/en-us/graph/blogs/announcing-30-days-of-microsoft-graph-blog-series/#


## Examples

```
https://graph.microsoft.com/v1.0/me/
https://graph.microsoft.com/v1.0/me/?$select=displayName,mail,jobTitle
https://graph.microsoft.com/v1.0/me/photo/$value
https://graph.microsoft.com/v1.0/me/messages
https://graph.microsoft.com/v1.0/me/memberOf
https://graph.microsoft.com/v1.0/me/manager
https://graph.microsoft.com/v1.0/me/calendar/events
https://graph.microsoft.com/v1.0/me/ownedDevices


https://graph.microsoft.com/v1.0/users
https://graph.microsoft.com/v1.0/users/{id}
https://graph.microsoft.com/v1.0/users?$select=displayName,mail,jobTitle
https://graph.microsoft.com/v1.0/users?$filter=givenName eq 'Adele'

https://graph.microsoft.com/v1.0/me/people/?$search="Joakim"
https://graph.microsoft.com/v1.0/me/people?$top=2000

https://graph.microsoft.com/v1.0/myorganization/domains 

```

---

# Batch with HTTP POST

```
POST https://graph.microsoft.com/v1.0/$batch
Accept: application/json
Content-Type: application/json

{
  "requests": [
    {
      "id": "1",
      "method": "GET",
      "url": "/me"
    },
    {
      "id": "2",
      "method": "GET",
      "url": "/me/photo/$value"
    }
  ]
}
```


```
POST https://graph.microsoft.com/v1.0/$batch

{
  "requests": [
    {
      "id": "1",
      "method": "GET",
      "url": "/me/?$select=displayName,mail,jobTitle"
    }
  ]
}
```

```
POST https://graph.microsoft.com/v1.0/$batch

{
  "requests": [
    {
      "id": "1",
      "method": "GET",
      "url": "/me/?$select=displayName,mail,jobTitle"
    }
  ]
}
```

```
POST https://graph.microsoft.com/v1.0/$batch

{
  "requests": [
    {
      "id": "1",
      "method": "POST",
      "url": "/me/people?$top=2000"
    }
  ]
}
```
