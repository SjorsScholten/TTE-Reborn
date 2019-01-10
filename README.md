# TTE-Reborn

Niet alles naar de master pushen. Maak gebruik van branches.
Weet je niet hoe branches werken? Vraag het aan een van je collegas

Variabelen in camelCase: int camelCase = 0;

Methoden: private int TestMethod()

Alle methoden zijn voorzien van een Summary

Schrijf je code zo herbruikbaar mogelijk dus als er bijvoorbeeld teleportatie van te pas komt kan dit door meerdere dingen gebruikt worden. Maak het dan niet specifiek voor 1 ding bruikbaar.

Indien we Unity gebruiken:
- Variabelen die je wil assignen via de editor maar die niet door aan ander script worden gebruikt worden private. Gebruik hiervoor het                                                                                             [SerializeField] attribuut.
- Gebruik het [Tooltip] attribuut om je variabelen toe te lichten. Zo kan je vanuit de editor al zien wat deze doet.
- Indien je lijst met editor variabelen te lang wordt ga dingen groeperen via het [Header] attribuut.
- Is je attribuut wel nodig in een ander script? maak er dan het liefst een private set van.
- Wordt dezelfde data op veel plekken gebruikt? probeer een scriptable object te maken.
- Vermijd spaghetti, gebruik Events om te notify-en.
- Wanneer handig gebruik regions om overzichtelijkheid te creëren.
