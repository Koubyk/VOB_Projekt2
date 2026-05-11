Komentár k riešeniu

Pre projekt som si zvolil konzolovú aplikáciu typu Weather Aggregator, ktorá získava dáta z externých HTTP zdrojov a ďalej ich spracováva. Používateľ zadá mestá, pre ktoré aplikácia najprv zistí súradnice a potom načíta aktuálne údaje o počasí.

Pri návrhu som sa snažil dodržať prehľadnú architektúru a oddeliť jednotlivé zodpovednosti. Program je rozdelený na časť pre používateľské rozhranie, aplikačnú logiku a časť pre komunikáciu s externými službami. Vďaka tomu je kód čitateľnejší a jednoduchšie rozšíriteľný.

Použil som vlastné triedy a rozhrania, aby aplikácia nebola naviazaná na jednu konkrétnu implementáciu. To mi umožnilo pracovať s providermi polymorfne a zároveň sa priblížiť princípom SOLID, hlavne oddeleniu zodpovedností a závislosti na abstrakciách.

Asynchrónne spracovanie som použil pri HTTP volaniach cez async/await, pretože ide o I/O operácie, kde to dáva zmysel. Pri viacerých nezávislých požiadavkách som využil aj Task.WhenAll, aby sa dáta načítali efektívnejšie.

Nad získanými dátami aplikácia nerobí len výpis, ale aj ďalšie spracovanie, napríklad porovnanie miest, výpočet priemernej teploty, hľadanie minima a maxima, filtrovanie a triedenie výsledkov.

Dôležitou časťou bolo aj ošetrenie chýb. Program reaguje na neplatný vstup, nenájdené mesto, timeout alebo chybu externej služby. Snažil som sa, aby sa výnimky zachytávali rozumne a aby aplikácia pri chybe nespadla bez vysvetlenia.

Konzolovú aplikáciu som zvolil preto, lebo pri tomto zadaní nebolo rozhranie hlavnou prioritou a mohol som sa viac sústrediť na návrh, architektúru a async časť programu.

Cieľom bolo vytvoriť funkčnú a prehľadnú aplikáciu, na ktorej je vidieť použitie OOP, vrstvenia, práce s HTTP API a asynchrónneho spracovania.