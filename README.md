# Epidemic Lab

Simulatore epidemiologico interattivo basato sul modello matematico SIR, realizzato con Blazor WebAssembly e .NET 10.

Il progetto nasce come elaborato di maturità nel 2020 ed è stato completamente rinnovato nel 2026 per renderlo nuovamente compilabile, autonomo e adatto ai browser moderni.

## Funzionalità

- simulazione dell'evoluzione di un'epidemia con il modello SIR;
- modifica in tempo reale di popolazione, infetti iniziali, beta, gamma e durata;
- calcolo automatico del numero di riproduzione di base `R₀`;
- grafico SVG responsive di suscettibili, infetti e rimossi;
- individuazione del giorno e del valore del picco degli infetti;
- scenari preconfigurati: contenuto, standard e aggressivo;
- tabella con i dati prodotti dalla simulazione;
- pagina didattica con equazioni, significato dei compartimenti e limiti del modello;
- funzionamento completamente locale, senza database, account o chiavi API.

## Tecnologie

| Area | Tecnologia |
| --- | --- |
| Interfaccia | Blazor WebAssembly |
| Framework | .NET 10 |
| Linguaggio | C# e Razor |
| Grafici | SVG nativo |
| Stile | CSS responsive personalizzato |
| Modello numerico | Runge-Kutta del quarto ordine |

Non vengono utilizzate librerie grafiche proprietarie o servizi esterni. Dopo il primo ripristino dei pacchetti .NET, la simulazione viene eseguita interamente nel browser.

## Requisiti

È sufficiente una delle seguenti configurazioni:

- Visual Studio 2026 con il workload per lo sviluppo ASP.NET e Web;
- .NET SDK 10 e un editor di codice a scelta.

Per controllare la versione installata:

```powershell
dotnet --version
```

## Avvio con Visual Studio

1. Aprire `EpidemicProject.sln`.
2. Attendere il completamento del ripristino NuGet.
3. Impostare `EpidemicProject.Client` come progetto di avvio, se non è già selezionato.
4. Premere `F5` per avviare con il debugger oppure `Ctrl+F5` per avviare senza debugger.

Il browser si aprirà automaticamente sull'indirizzo locale assegnato da Visual Studio.

## Avvio da terminale

Dalla cartella principale del repository:

```powershell
dotnet restore .\EpidemicProject.sln
dotnet run --project .\EpidemicProject.Client
```

Aprire nel browser l'indirizzo indicato dal comando, generalmente simile a `http://localhost:5000`.

## Compilazione e pubblicazione

Compilazione della soluzione:

```powershell
dotnet build .\EpidemicProject.sln --configuration Release
```

Produzione dei file statici distribuibili:

```powershell
dotnet publish .\EpidemicProject.Client --configuration Release
```

L'output viene generato in:

```text
EpidemicProject.Client/bin/Release/net10.0/publish/wwwroot/
```

## Struttura della soluzione

```text
EpidemicProject.sln
├── EpidemicProject.Client/       Applicazione Blazor WebAssembly
│   ├── Components/               Grafico SVG e componenti riutilizzabili
│   ├── Pages/                    Simulatore e pagina sul modello
│   ├── Shared/                   Layout e navigazione
│   └── wwwroot/                  CSS e risorse pubbliche
└── ClassLibrary1/                Libreria del dominio, assembly DAL
    ├── Models/                   Risultati della simulazione
    └── SimulationModels/         Implementazione del modello SIR
```

Le altre cartelle presenti nel repository appartengono agli esperimenti originali del 2020 e non fanno parte della soluzione eseguibile principale.

## Il modello SIR

Il modello divide una popolazione chiusa in tre compartimenti:

- `S` — persone suscettibili all'infezione;
- `I` — persone infette e contagiose;
- `R` — persone rimosse dalla catena di trasmissione.

La loro evoluzione è descritta dalle equazioni:

```text
dS/dt = -βSI/N
dI/dt =  βSI/N - γI
dR/dt =  γI
```

Dove:

- `β` rappresenta il coefficiente di contagio;
- `γ` rappresenta il coefficiente di rimozione;
- `N` è la popolazione totale;
- `R₀ = β / γ` è il numero di riproduzione di base nelle condizioni iniziali del modello.

L'applicazione integra numericamente le equazioni con il metodo Runge-Kutta del quarto ordine e registra un punto per ogni giorno simulato.

## Limiti scientifici

Epidemic Lab è uno strumento didattico, non un sistema di previsione sanitaria.

Il modello SIR utilizzato assume una popolazione chiusa, contatti omogenei, parametri costanti e immunità dopo la rimozione. Non considera età, territorio, vaccinazioni, varianti, interventi sanitari o cambiamenti nel comportamento individuale.

## Modernizzazione del progetto

La revisione del 2026 ha introdotto:

- migrazione da Blazor WebAssembly 3.2 e .NET Framework 4.7.2 a .NET 10;
- rimozione delle dipendenze obsolete DevExpress, Radzen e ML.NET;
- rimozione della dashboard collegata alla vecchia API `covid19api.com`;
- sostituzione del metodo di Eulero con Runge-Kutta del quarto ordine;
- validazione degli input e gestione degli errori;
- nuova interfaccia responsive e accessibile;
- compilazione Debug e pubblicazione Release senza errori o avvisi.

La relazione scolastica originale e tutte le versioni precedenti restano disponibili nella cronologia Git del repository.

## Licenza e utilizzo

Progetto personale e didattico di Luca Pedersoli. Prima di riutilizzarlo o distribuirlo, aggiungere al repository una licenza esplicita coerente con l'utilizzo desiderato.
