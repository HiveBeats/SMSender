# SMSender 

An implementation for FDX test task 'SMS' (fullstack).

---
## Requirements

To run the solution, you should have 'docker-compose' installed.

---
## Project Setup

```sh
docker-compose up --build
```

---
## Available endpoints

### Frontend

http://localhost/

---
### API

 - POST to http://localhost/api/ShortMessage/ should accept that kind of body:
```json
{
    "from": "", 
    "to": [""], 
    "content": ""
}
```
and send that SMS to queue

 - GET to http://localhost/api/ShortMessage/All should return all SMS in the database.

 - GET to http://localhost/api/ShortMessage/{id} should return detailed information for SMS

---

