# Watson API - NSS Back-End Capstone 
>By Dre Randaci

<br>

**Summary**: NSS Back-end capstone API that connects users with IBM Watson's Visual Recognition API. Broken into two projects, a Web API built on C#/.NET and a React Native mobile client, the API allows developers to send multipart/form-data containing picture URI's across HTTP and receive prediction classifications from IBM Watson in JSON formatted responses. 
>Mobile client [here](https://github.com/DreRandaci/Backend-Capstone-Client) 

<hr>

### Description
Consumes multipart/form-data, sends that data to IBM Watson and responds with JSON prediction classifications. Hosted on DigitalOcean using NGINX and the Ubuntu OS. 

# API Resources
>**NOTE** the key of `file` for the form-data is required. 

>Supports `jpg`, `jpeg`, and `png` picture formats. 

### General classifications 
Use this endpoint to send a picture URI in multipart/form-data request directly from a device.

- Endpoint: 
 http://watson.drerandaci.com/api/prediction/ClassifyGeneric

- Example POST Request:
```javascript
const data = new FormData(); 

data.append('file', {
    uri: 'location of the image on the device{.jpg, .jpeg, or .png}',
    type: 'image/jpg', 
    name: 'test1'
});           

fetch(`http://watson.drerandaci.com/api/Prediction/ClassifyGeneric`, {
    method: 'POST',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'multipart/form-data;'
    },
    body: data
});
```

- Example Response
```json
[
    {
        "class": "redfish",
        "score": 0.833,
        "type_hierarchy": "/animal/aquatic vertebrate/fish/salmon/redfish"
    },
    {
        "class": "salmon",
        "score": 0.833,
        "type_hierarchy": null
    },
    {
        "class": "fish",
        "score": 0.969,
        "type_hierarchy": null
    },
    {
        "class": "aquatic vertebrate",
        "score": 0.969,
        "type_hierarchy": null
    },
    {
        "class": "animal",
        "score": 0.969,
        "type_hierarchy": null
    },
    {
        "class": "red drum",
        "score": 0.815,
        "type_hierarchy": "/animal/aquatic vertebrate/fish/spiny-finned fish/red drum"
    },
    {
        "class": "spiny-finned fish",
        "score": 0.815,
        "type_hierarchy": null
    }
]
```

### General classifications from URL's 
Use this endpoint to get prediction data from a picture URL. 

- Endpoint: 
 http://watson.drerandaci.com/api/prediction/ClassifyGenericUrl

- Example POST Request:
```javascript
fetch(`http://watson.drerandaci.com/api/prediction/ClassifyGenericUrl?url=${url}`, {
    method: 'POST'
});
```

- Example Response:
```json
[
    {
        "name": "default",
        "classifier_id": "default",
        "classes": [
            {
                "class": "Appenzeller dog",
                "score": 0.944,
                "type_hierarchy": "/animal/domestic animal/dog/Appenzeller dog"
            },
            {
                "class": "dog",
                "score": 0.971,
                "type_hierarchy": null
            },
            {
                "class": "domestic animal",
                "score": 0.972,
                "type_hierarchy": null
            },
            {
                "class": "animal",
                "score": 0.973,
                "type_hierarchy": null
            },
            {
                "class": "Sennenhunde dog",
                "score": 0.5,
                "type_hierarchy": "/animal/domestic animal/dog/Sennenhunde dog"
            }
        ]
    }
]
```

### Detect Faces 
Use this endpoint to send a picture URI that contains a person's face in multipart/form-data request directly from a device. 

- Endpoint: 
 http://watson.drerandaci.com/api/prediction/DetectFaces

- Example POST Request:
```javascript
const data = new FormData(); 

data.append('file', {
    uri: 'location of the image on the device{.jpg, .jpeg, or .png}',
    type: 'image/jpg', 
    name: 'test1'
});           

fetch(`http://watson.drerandaci.com/api/Prediction/DetectFaces`, {
    method: 'POST',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'multipart/form-data;'
    },
    body: data
});
```

- Example Response:
```JSON
[
    {
        "age": {
            "min": 18,
            "max": 24,
            "score": 0.66328
        },
        "gender": {
            "gender": "FEMALE",
            "score": 0.577276
        },
        "face_location": {
            "width": 770,
            "height": 891,
            "left": 1377,
            "top": 934
        },
        "identity": null
    }
]
```

### Detect Faces from URL's 
Use this endpoint to get prediction data from a picture URL containing faces.  

- Endpoint: 
 http://watson.drerandaci.com/api/prediction/DetectFacesUrl

- Example POST Request:
```javascript
fetch(`http://watson.drerandaci.com/api/prediction/DetectFacesUrl?url=${url}`, {
    method: 'POST'
});
```

- Example Response:
```json
[
    {
        "age": {
            "min": 35,
            "max": 44,
            "score": 0.403753
        },
        "gender": {
            "gender": "MALE",
            "score": 0.989013
        },
        "face_location": {
            "width": 439,
            "height": 519,
            "left": 381,
            "top": 340
        },
        "identity": {
            "name": "David Hasselhoff",
            "score": 0.731059,
            "type_hierarchy": "/people/guests/celebrities/david hasselhoff"
        }
    }
]
```

<hr>

## Demo

View the mobile client [here](https://github.com/DreRandaci/Backend-Capstone-Client) 