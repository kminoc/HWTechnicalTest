# HWTechnicalTest
Test Technique pour Hellowork


## A propos
Le projet est une WebAPI .net 8. 
Une fois l'application démarrée, un background service récupère les Offres d'emplois via l'[api France Travail](https://api.francetravail.io/partenaire/offresdemploi/v2/offres/search) et stock les stock dans une base de donnée Mongo.

## API endpoints
- [GET] /Offers : récupère toutes les offres dans la collection Mongo
- [GET] /Offers/{id} : récupère le contenu d'une offre via son identifiant
- [GET] /Offers/Count : nombre d"offres dans la BDD
- [GET] /Offers/Overview : récupère un résumé des offres

## Technologies
- C#
- .NET 8

### Librairies utilisées
- Serilog
- MongoDB
- Swagger

## Améliorations
- Améliorer la gestion des erreurs
- Améliorer la gestion des logs
- Améliorer la pagination
- Gestion du cache
- Tests unitaires

## Prérequis
- .NET 8 SDK
- Une base donnée Mongo
- Des identifiants pour l'utilisation de l'api France Travail

## Usage
1. cloner le repo : `git clone https://github.com/kminoc/HWTechnicalTest.git` 
2. renseigner dans appsettings.json :
     - La chaine de connexion à la BDD Mongo
     - les client_id et client_secret pour l'accès à l'api France Travail
3. accès à l'api via swagger : (http://localhost:5088)
