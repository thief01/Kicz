# Kich Application

Kich application is twitter like application to create posts like them and follow friends. I'm creating it for Polish Twitch Community. I wanted to make some Apps for news like "Szklanka2YJ" Youtube channel but text-based. 


# Usage
```bash
// clone repository
git clone

// change folder
cd Kich

// run docker
docker compose -f docker-compose-dev.yml up

// or for prod (Traefik or Nginx configuration needed)
docker compose up

// visit this URL to create your account
localhost:3000/register

// write your first post
localhost:3000/feed

// Swagger DOC
localhost:8000/swagger/index.html

```

# Quick Look

[Example Kicz APP](https://kicz.mimigames.pl/feed)

## Register page
<img width="897" height="1273" alt="image" src="https://github.com/user-attachments/assets/d1a80aed-dfaa-470a-9f39-6b316c9ce903" />

## Feed page 1
<img width="904" height="1279" alt="image" src="https://github.com/user-attachments/assets/bfdb7cd0-0e6c-4ef5-a818-c44a19e47178" />

## Feed page 2
<img width="728" height="703" alt="image" src="https://github.com/user-attachments/assets/0d7eba3e-03df-447e-9817-05f9b093757f" />

## Swagger Doc
<img width="1575" height="1054" alt="image" src="https://github.com/user-attachments/assets/15912c49-f362-4973-a71d-238e1ac69cfa" />

