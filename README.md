```
Esame maturit`a 2020
```
# UTILIZZO DEL MODELLO SIR PER

# EFFETTUARE SIMULAZIONI

# EPIDEMIOLOGICHE CON ML.NET, ANALISI

# DATI FORNITI DA UN API TRAMITE

# PROTOCOLLO HTTP/S.

```
30 giugno 2020
```
## Pedersoli Luca

## luca.pedersoli@mail.polimi.it

## ITCG “OLIVELLI“ - IPSSAR “PUTELLI“

## http://www.olivelliputelli.it


## Indice

- 1 Descrizione del progetto
   - 1.1 Introduzione
   - 1.2 Obiettivi principali
   - 1.3 Strumenti utilizzati
- 2 Blazor Wasm VS Blazor Server
   - 2.1 Wasm
   - 2.2 Server
   - 2.3 Scelta
- 3 Modello SIR
   - 3.1 Introduzione
   - 3.2 Rappresentazione matematica
- 4 Struttura progetto
   - 4.1 Struttura
   - 4.2 Diagramma
- 5 Implementazione modello SIR, e uso dell’intelligenza artificiale per analisi risultati
   - 5.1 Modelli
   - 5.2 Implementazione della simulazione
   - 5.3 Costruzione pagina
- 6 Implementazione pagina statistiche Covid-19
   - 6.1 Modelli
   - 6.2 Implementazione HttpClient
   - 6.3 Costruzione pagina
- 7 Ottimizzazione
   - 7.1 Caricamento
   - 7.2 Componenti
   - 7.3 Responsive
- 8 Il protocollo HTTP
   - 8.1 Un po’ di storia
   - 8.2 Caratteristiche
   - 8.3 Come funziona?
   - 8.4 Struttura richiesta e risposta
   - 8.5 Tipo di richiesta
   - 8.6 Codici
- 9 La sicurezza nel protocollo HTTP, (HTTPS)
   - 9.1 Idea di fondo
   - 9.2 SSL / TLS
- 10 Conclusione
   - 10.1 Futuri miglioramenti


## 1 Descrizione del progetto

### 1.1 Introduzione

Con il dilungarsi dell’emergenza Coronavirus (Covid-19), molti si sono domandati quali strumenti
scientifici esistono per prevedere il propagarsi di una epidemia, e in quale modo `e possibile
mantenere sotto controllo l’evolversi di quest’ultima. Il progetto a breve presentato cercher`a
di rispondere a questi quesiti e prover`a a illustrare in maniera chiara ed efficace gli strumenti
necessari che la tecnologia ci mette a disposizione per definire un quadro almeno approssimativo
della situazione futura.

### 1.2 Obiettivi principali

Gli obiettivi che questo progetto si pone sono:

- Realizzare una pagina che permetta la simulazione di una generica epidemia, attraverso
    valori personalizzabili, sfruttando come base il modello SIR di Kermack e McKendrick.
- Sfruttare l’intelligenza artificiale per perfezionare la previsione del modello e implementare
    un valore che permetta di valutare l’affidabilit`a del modello sviluppato.
- Realizzare una pagina dedicata al Covid-19 dove `e possibile visualizzare le principali
    statistiche riguardo all’evoluzione della pandemia.

### 1.3 Strumenti utilizzati

Gli strumenti (tecnologici e non) che useremo per realizzare questo progetto sono:

- ASP.NET Core Blazor WebAssembly,^1 `e un Framework single-page e component-based per
    la creazione di app Web interattive sul lato client con .NET.
- Bootsrap,^2 framework che contiene modelli di progettazione basati su HTML e CSS, sia
    per la tipografia, che per le varie componenti dell’interfaccia, come moduli, pulsanti e
    navigazione, cos`ı come alcune estensioni opzionali di JavaScript.
- Radzen,^3 libreria che mette a disposizione alcuni componenti, utilizzata nel progetto per
    l’autocompletamento, e le grid.
- DevExpress,^4 libreria che mette a disposizione alcuni componenti, utilizzata nel progetto
    per i grafici.
- ML.NET,^5 framework .net che permette l’implementazione di strumenti di intelligenza
    artificiali.
- Covid-19 API,^6 API Web che mette a disposizione tutti i dati sui contagi dall’inizio pandemia,
    utilizzata nel progetto per le statistiche.

(^1) Microsoft.Introduzione a ASP.NET Core Blazor.url:https://docs.microsoft.com/it-it/aspnet/core/
blazor/?view=aspnetcore-3.1.
(^2) Mark Otto e Jacob Thornton.Bootstrap.url:https://getbootstrap.com/.
(^3) Mike Prager.Rapid Application Development for the Web: Radzen.url:https://www.radzen.com/.
(^4) Native Blazor Components powered by DevExpress. url: https : / / demos. devexpress. com / blazor /
ChartSeriesTypes.
(^5) ML.NET: Machine Learning made for .NET.url:https://dotnet.microsoft.com/apps/machinelearning-
ai/ml-dotnet.
(^6) url:https://api.covid19api.com/.


- Coronavirus tracking,^7 mappa che mostra la diffusione del contagio, utilizzata nel progetto
    per fornire un’interfaccia grafica tramite una mappa su come si sia diffuso il contagio.
- Modello SIR di Kermack e McKendrick,^8 `e un modello matematico sviluppato nel 1927 dagli
    scienziati William Ogilvy Kermack e Anderson Gray McKendrick.^9 Questo archetipo sar`a
    destinato a diventare uno dei principali punti di riferimento per lo studio e l’elaborazione
    dei modelli di diffusione epidemica.
- C#, HTML, CSS, tecnologie di base su cui si basano tutti gli strumenti sopra illustrati.

## 2 Blazor Wasm VS Blazor Server

Per la scelta della tecnologia da usare sono stati presi in considerazione una serie di vantaggi e
svantaggi^10 offerte da entrambi le tecnologie:

### 2.1 Wasm

Pro Wasm:

- Non ci sono dipendenze dal server, una volta scaricata l’app `e completamente funzionante
- Le risorse e le funzionalit`a dei client sono sfruttate appieno.
- Il lavoro viene scaricato dal server al client.
- Un server Web ASP.NET Core non `e necessario per ospitare l’app. Scenari di distribuzione
    senza server sono possibili (ad esempio, la gestione dell’app da una rete CDN).

Contro Wasm:

- L’app `e limitata alle funzionalit`a del browser.
- E necessario utilizzare hardware e software client in grado di supportare questa tecnologia.`
- Le dimensioni del download sono maggiori e il caricamento delle app richiede pi`u tempo.
- Il supporto di runtime e strumenti .NET `e meno maturo. Ad esempio, esistono limitazioni
    nel supporto e nel debug di .NET Standard.
- C’`e una perenne esposizione della logica back-end del codice.

### 2.2 Server

Pro Server:

- Le dimensioni del download sono significativamente inferiori rispetto a un’app Blazor
    WebAssembly e l’app viene caricata molto pi`u velocemente.

(^7) Tracking Coronavirus COVID-19.url:https://app.developer.here.com/coronavirus/.
(^8) Roberto Natalini.La matematica delle epidemie: istruzioni per l’uso.url:http://maddmaths.simai.eu/
divulgazione/focus/epidemie-matematica/.
(^9) Modello Kermack-McKendrick. 2020.url:https://it.wikipedia.org/wiki/Modello_Kermack-McKendrick.
(^10) Guardrex.ASP.NET Core Blazor hosting models.url:https://docs.microsoft.com/en-us/aspnet/core/
blazor/hosting-models?view=aspnetcore-3.1.


- L’app sfrutta appieno le funzionalit`a del server, incluso l’uso di qualsiasi API compatibile
    con .NET Core.
- .NET Core nel server viene usato per eseguire l’app, pertanto gli strumenti .NET esistenti,
    ad esempio il debug, funzionano come previsto.
- Sono supportati i thin client. Ad esempio, le app Blazor Server funzionano con browser che
    non supportano WebAssembly e su dispositivi con risorse limitate.
- La base di codice .NET resta nascosta nel server.

Contro Server:

- Esiste una latenza pi`u elevata. Ogni interazione dell’utente comporta un hop di rete.
- Non `e disponibile alcun supporto offline. Se la connessione client non riesce, l’app smette di
    funzionare.
- La scalabilit`a `e complessa per le app con molti utenti. Il server deve gestire pi`u connessioni
    client e gestire lo stato client.
- Per gestire l’app `e necessario un server ASP.NET Core.

### 2.3 Scelta

Nel realizzare questo progetto `e stato scelto l’utilizzo di WASM, ritenendo che la limitazione
dell’app al browser o all’hardware non sia un problema per le operazioni che deve svolgere, inoltre
la dimensione del download si aggira intorno a qualche MB e produce un attesa molto piccola.

## 3 Modello SIR

### 3.1 Introduzione

Per realizzare questo progetto verr`a usato il modello SIR di Kermack e McKendrick,^11 quest’ultimo
`e uno dei pi`u semplici e anche se contiene ipotesi chiaramente irrealistiche, i concetti introdotti
tramite esso risultano essenziali per fornire una prima intuizione sulla dinamica delle epidemie,
intuizione che rimane confermata in modelli pi`u complessi, sia pure con numerose modifiche. Tale
modello prevede la divisione della popolazione in compartimenti, quello dei suscettibili S ovvero
gli individui sani che possono essere infettati, gli infetti I e i rimossi R ovvero coloro che sono
guariti, immuni e anche i deceduti. La versione ideata da Kermack e McKendrick `e estremamente
versatile e facile, non implementando fattori esterni e prendendo in considerazione solo numeri
ristretti.

(^11) Natalini,La matematica delle epidemie: istruzioni per l’uso, cit.


### 3.2 Rappresentazione matematica

```
Il modello SIR di Kermack e McKendrick^12 `e un sistema di equazioni differenziali:
```
```
dS
dt
```
#### =−

```
βIS
N
```
#### ,

```
dI
dt
```
#### =

```
βIS
N
−γI,
```
```
dR
dt
```
```
=γI,
```
#### (1)

```
Dove S `e il numero di suscettibili, I il numero di infetti, R il numero di rimossi, N `e la popolazione
totale e quindi la somma di queste tre variabili; Inoltre ci sono altri due parametri,βche
rappresenta ci`o che `e normalmente chiamato ”parametro di infettivit`a”, eγche indica invece il
cosiddetto ”parametro di transazione”, ovvero la possibilit`a di passare ad uno stato di rimosso.
La seguente immagine^13 illustra il funzionamento in maniera precisa:
```
## 4 Struttura progetto

Il progetto `e strutturato in due parti principali: DAL (Data Access Layer) e EpidemicProject.Client
(Blazor Wasm) nel DAL verranno inseriti tutti i modelli relativi all’API (Models) e alle simulazioni
(SimulationModel); Mentre nell’EpidemicProject.Client (Blazor Wasm) troviamo l’applicazione
Blazor Client che contiene:^14

(^12) Compartmental models in epidemiology. 2020.url:https://en.wikipedia.org/wiki/Compartmental_models_
in_epidemiology.
(^13) Kai Sasaki.COVID-19 dynamics with SIR model. 2020.url:https://www.lewuathe.com/covid-19-dynamics-
with-sir-model.html.
(^14) Guardrex.ASP.NET Core Blazor templates.url:https://docs.microsoft.com/en-us/aspnet/core/blazor/
templates?view=aspnetcore-3.1.


### 4.1 Struttura

- wwwroot, dove vengono inseriti i file statici;
- Components, dove sono inseriti tutti i componenti
    utilizzati in questa applicazione;
- Pages, dove vengono inserite le pagine del progetto;
- Shared, dove vengono inserite quelle componenti
    condivise in tutto il programma;
- Imports.razor, dove vengono inseriti gli using che
    saranno disponibili in tutta l’app;
- App.razor, dove viene inserito il routing e il layout
    di base;
- Program.cs, dove viene inizializzata l’app e dove ven-
    gono registrati i servizi, di quest’ultimi le applicazioni
Wasm mettono a disposizione di base l’HttpClient
per la chiamata di API.

### 4.2 Diagramma

Per una mancanza di tempo, e per la grande complessit`a del progetto sviluppato, ho evitato
di proporre diagrammi UML, o dei casi d’uso, che risultavano infine troppo complessi sia da
realizzare che da spiegare. Viene preso cos`ı inconsiderazione un altro diagramma che ha il compito
di riassumere in maniera rapida e intuitiva i grafici sopra citati:


## 5 Implementazione modello SIR, e uso dell’intelligenza artificiale per analisi risultati

### 5.1 Modelli

Realizziamo prima di tutto i modelli che verranno usati per la creazione di questa pagina:

public class SIRStat
{
public SIRStat(double ds, double di, double dr, double dt)
{
Ds = ds;
Di = di;
Dr = dr;
Dt = dt;
}
public double Ds { get; set; }
public double Di { get; set; }
public double Dr { get; set; }
public double Dt { get; set; }
}

Il passo successivo `e quello di sfruttare il Model Builder^15 messo a disposizione da Microsoft per
realizzare il nostro scheletro per l’intelligenza artificiale.

```
Ci verr`a fornito un ambiente grafico che produrr`a automaticamente il modello che ci serve.
```
(^15) Bri Achtman et al.ML.NET Model Builder is now a part of Visual Studio. 2020.url:https://devblogs.
microsoft.com/dotnet/ml-net-model-builder-is-now-a-part-of-visual-studio/.


Il sistema si occupa anche di addestrare automaticamente il modello, basta fornirgli un file csv
con degli esempi; Una volta completati i passaggi esso verr`a generato automaticamente:

public class Predizione
{
[ColumnName("Score")]
public int Rate { get; set; }
[ColumnName("Lista")]
public List<SIRStat> Stats { get; set; } = new List<SIRStat>();
}
public class AiModel
{
public Predizione Prediction { get; set; }
public void Previsione(List<SIRStat> sir)
{
MLContext mlContext = new MLContext();
DataViewSchema predictionPipelineSchema;
ITransformer predictionPipeline = mlContext.Model.Load("model.zip", out predictionPipelineSchema);
PredictionEngine<List<SIRStat>, Predizione> predictionEngine =
mlContext.Model.CreatePredictionEngine<List<SIRStat>, Predizione>(predictionPipeline);
List<SIRStat> inputData = sir;
Prediction = predictionEngine.Predict(inputData);
}
}

### 5.2 Implementazione della simulazione

Una volta creati correttamente i modelli dobbiamo unire il tutto per fare in modo che ci venga
generata una lista di risultati, per fare ci`o costruiamo una classe il cui compito `e quello di
computare tutte le operazioni, essa `e molto lunga metter`o solo i punti salienti. Definiamo le
propriet`a che ci servono per generare i dati:

public double Scale { get; private set; } = 0.1;
public double TotalPopulation {get; set;}
public double SusceptiblePopulation {get; set;}
public double InfectedPopulation {get; set;}


public double RemovedPopulation {get; set;}
public double Time {get; set;}
public double Alpha {get; set;}
public double Beta {get; set;}

Adesso applichiamo le formule per calcolare il numero di suscettibili, di infetti e di rimossi in un
momento T:

double dS = (-Beta * SusceptiblePopulation * InfectedPopulation) / TotalPopulation;
double dI = ((Beta * SusceptiblePopulation * InfectedPopulation) / TotalPopulation) - Alpha * InfectedPopulation;
double dR = Alpha * InfectedPopulation;
double dt = 1;
SusceptiblePopulation = SusceptiblePopulation + dS * Scale;
InfectedPopulation = InfectedPopulation + dI * Scale;
RemovedPopulation = RemovedPopulation + dR * Scale;
Time = Time + dt * Scale;

facciamo in modo che restituisca tutti i valori di un intervallo di tempo (1≤t≤100) in una lista
di SIRStat, che rappresenta quindi i suscettibili, i rimossi e gli infetti in un dato momento t.

### 5.3 Costruzione pagina

Il risultato finale che otterremo sar`a questo:

I bottoni Dati e Formule aprono delle Card che contengono queste informazioni, i vari form
sono legati alle property con i bind, per evitare di dilungarci troppo verranno analizzati solo i


charts. Utilizziamo la libreria Radzen^16 per implementare i grafici, ChartsData conterr`a i dati,
che devono essere IQueryable:

public List<SIRStat> Stat { get; set; } = new List<SIRStat>();
IQueryable<SIRStat> ChartsData = Stat.AsQueryable();

A questo punto definiamo il grafico:

ArgumentField corrisponde all’asse delle x che in questo caso `e il tempo, ValueField corrisponde
invece all’asse delle y ovvero il numero di infetti, rimossi o suscettibili. L’evento onclick fa’ in
modo che quando si clicchi sul grafico si apra una card a tutto schermo per mostrare un’analisi
pi`u dettagliata:

## 6 Implementazione pagina statistiche Covid-

La pagina successiva del progetto ha il compito di mostrare le ultime statistiche relative al
Covid-19, attraverso un API pubblica.

(^16) Prager,Rapid Application Development for the Web: Radzen, cit.


### 6.1 Modelli

Utilizzando lo strumento^17 JSON TO C# convertiamo il JSON fornito dall’API in modelli C#.
Questo `e il JSON per effettuare il GET di tutte le nazioni:

public class CountriesModel
{
public string Country { get; set; }
public string Slug { get; set; }
public string Iso2 { get; set; }
}

Questo `e il JSON per effettuare il GET dalla nazione selezionata:

public class CountryModels
{
public string Country { get; set; }
public string CountryCode { get; set; }
public string Province { get; set; }
public string City { get; set; }
public string CityCode { get; set; }
public string Lat { get; set; }
public string Lon { get; set; }
public long Confirmed { get; set; }
public long Deaths { get; set; }
public long Recovered { get; set; }
public long Active { get; set; }
public DateTimeOffset Date { get; set; }
}

### 6.2 Implementazione HttpClient

Un vantaggio di utilizzare Blazor Client `e quello di avere il servizio HttpClient gi`a configurato:

public static async Task Main(string[] args)
{
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddDevExpressBlazor();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
await builder.Build().RunAsync();
}

Per poter utilizzare l’HttpClient iniettiamo il servizio:

@inject HttpClient Http;

Implementiamo ora il metodo per il GET di tutte le nazioni:

protected override async Task OnInitializedAsync()
{
listaNazioni = await Http.GetFromJsonAsync<CountriesModel[]>("https://api.covid19api.com/countries");
}

E anche quello che restituisce i dettagli di un singolo stato:

(^17) Instantly generate CSharp models and helper methods from JSON..url:https://quicktype.io/csharp/.


public async void ClickButton()
{
GraphState = true;
NazioneSceltaData.Clear();
NazioneSceltaData = (await Http.GetFromJsonAsync<CountryModels[]>
("https://api.covid19api.com/country/" + Nazione)).ToList();
NazioneSceltaDataQuery = NazioneSceltaData.AsQueryable();
StateHasChanged();
}

### 6.3 Costruzione pagina

Il risultato finale che otterremo sar`a questo:

Una volta inserit`a la nazione tramite form con autocompletamento, verranno visualizzati alcuni
dettagli generali e altri specifici:




Per implementare il bottone con l’autocompletamento sfruttiamo sempre la libreria Radzen
che mette a disposizione questa funzione partendo da un IEnumerable:

Utilizzando Bootstrap, abbiamo inserito due card con un link a Wikipedia, successivamente
troviamo alcune informazioni relative alla posizione della nazione scelta; e anche tre grafici che
descrivono la situazione attuale del contagio. Nella conclusione `e stata realizzata una tabella,
inizialmente nascosta per evitare che appaia troppo lunga, che rappresenta i valori specifici al
passare dei giorni:

public bool Stato { get; set; } = false;
<button class="btn btn-outline-primary" @onclick="() => Stato = !Stato" style="width: 100%;
margin-bottom: 20px; margin-top:10px">Espandi</button>
<div style="visibility:@(Stato?"visible":"hidden")">

Mentre l’ordinamento della tabella `e stato realizzato nel seguente modo:

Nel realizzare questo progetto `e stato preferito l’uso di LINQ per rendere qualsiasi operazione
il pi`u comprensibile possibile.

## 7 Ottimizzazione

### 7.1 Caricamento

Al momento, uno dei limiti pi`u gravosi di Blazor WebAssembly `e la necessita di caricare tutte
le dipendenze sotto forma di DLL per poter utilizzare C# nel browser. Una volta avviato il
progetto in forma di DEBUG e aprendo la console del browser si pu`o notare come le dimensioni
del download siano intorno ai 16MB. Fortunatamente durante se avviato in versione di release la
dimensione si riduce quasi del 50% ma rimane pur sempre un limite sopratutto considerando altri


framework come ad esempio Angular o Vue, le cui dimensioni sono molto inferiori. Purtroppo
non `e possibile sopperire a questo problema ma si pu`o rendere il loading iniziale pi`u carino
e piacevole dando l’idea che l’applicazione si stia caricando e non si blocchi. Ho utilizzato il
sito https://codepen.io per cercare un loading adeguato ai miei gusti e che non facesse uso di
JavaScript ma solo di animazioni CSS3, ho evitato JS non perch`e non sia utilizzabile ma per
evitare di complicare troppo la cosa. Una volta scelta l’animazione migliore, ho sostituito il
loading nell’app con i div e le classi, inoltre ho modificato il CSS situato nel WWWROOT,
aggiungendo le classi necessarie. Questo come gi`a detto non risolve il problema della quantit`a di
dati da caricare ma rende pi`u interessante la schermata di caricamento, e pu`o essere fondamentale
per mantenere alta l’attenzione dell’utente, che in media decide in pochi secondi se l’applicazione
gli piace oppure no.

### 7.2 Componenti

Per ottimizzare al meglio la nostra applicazione, sono stati realizzati alcuni componenti da poter
poi riutilizzare pi`u volte. Un componente `e un ”pezzo di applicazione” la cui unica responsabilit`a
`e di gestire la sua parte di interazione con l’utente e reagire ad essa. Un componente conterr`a
la definizione del pezzo di interfaccia che lo interessa, l’HTML nel caso di applicazioni Web, e
definir`a il codice per gestire questo pezzo di interfaccia, limitandosi a visualizzare i dati richiesti
e catturare l’input dell’utente. Questo significa che in un componente non ci dovrebbe mai
essere logica applicativa, ma solo logica di interfaccia, demandando tutti il resto ad altri elementi
dell’applicazione. I framework che usano questa struttura vengono detti Component-Based, e
nel mondo moderno tutti i maggiori framework seguono questo schema, da Angular fino a Vue.
Ovviamente i vantaggi sono molteplici, prima di tutto la leggibilit`a del codice che risulta pi`u
chiara e intuitiva; Il lavoro di gruppo risulta pi`u semplice perch`e ognuno pu`o concentrarsi sul
proprio componente senza rischiare di intaccare troppo il lavoro altri; Nel caso di un futuro cambio
di framework o un futuro aggiornamento, `e molto pi`u facile effettuare le modifiche evitando gravi
problemi che una architettura monolitica porta con se.

### 7.3 Responsive

Negli ultimi anni con la diffusione di ogni genere di schermo e di dispositivo diventa sempre pi`u
importante adattare le nostre applicazioni ad ogni screen, stavolta ci viene in aiuto bootstrap
i cui elementi sono gi`a spesso responsive cosi come la navbar laterale. Inoltre si `e fatto largo
uso delle grid per implementare un sistema di posizionamento degli elementi il pi`u automatico
possibile. I siti tradizionali possono anche risultare molto pesanti da caricare per i dispositivi
mobili in quanto, nonostante i progressi della tecnologia, le prestazioni offerte e la velocit`a di
connessione non sono sempre paragonabili a quelle di computer e notebook. Infine, un sito non
progettato correttamente potrebbe non essere completamente fruibile mediante dispositivi mobili.
Si pensi ad esempio ad un eCommerce nel quale non `e possibile effettuare il checkout mediante
smartphone. Ci`o si traduce inevitabilmente in una perdita immediata di molte opportunit`a di
business.


## 8 Il protocollo HTTP

### 8.1 Un po’ di storia

Le origini^18 di Internet si trovano in ARPANET, una rete di computer costituita nel settembre
del 1969 negli USA da ARPA (Advanced Research Projects Agency). Essa fu creata nel 1958 dal
Dipartimento della Difesa degli Stati Uniti per dare modo di ampliare e sviluppare la ricerca,
soprattutto all’indomani del sorpasso tecnologico dell’Unione Sovietica, che lanci`o il primo satellite
(Sputnik) nel 1957, conquistando i cieli americani. Il dispiegamento delle potenzialit`a di Internet
e la sua progressiva diffusione popolare sono per`o frutto dello sviluppo del WWW, il World Wide
Web, un sistema per la condivisione di informazioni in ipertesto del 1990 sviluppato da Tim
Berners-Lee presso il CERN (Centro Europeo per la ricerca nucleare). Dovremo aspettare fino al
1994 perch ́e il primo prototipo di Http/s venga realizzato, basato sul protollo SSL.

### 8.2 Caratteristiche

```
Il protocollo HTTP si basa sull’architettura client-server, ovvero un’architettura di rete nella
quale genericamente un computer client o terminale si connette ad un server per la fruizione di
un certo servizio, quale ad esempio la condivisione di una certa risorsa hardware/software con
altri client, appoggiandosi alla sottostante architettura protocollare.
```
```
Il protocollo `e situato nel livello pi`u alto della pila TCP/IP, ovvero il livello applicativo;
```
(^18) Storia di Internet. 2020.url:https://it.wikipedia.org/wiki/Storia_di_Internet.


### 8.3 Come funziona?

Il protocollo Http si basa su uno scambio di informazioni attraverso richieste e risposte, per
eseguire correttamente il caricamento di una pagina web si devono seguire una serie di passaggi
ben definiti:

- Il client analizza l’URL (Uniform Resource Locator), e estrae il dominio dove la risorsa `e
    localizzata;
- Il client avvia una connessione TCP (sicura, controllo errori, pi`u lenta) e invia una richiesta
    GET;
- Il server verifica il corretto ricevimento dell’oggetto e ne invia la relativa risposta;
- Il server invia la richiesta di chiusura che avverr`a solo nel caso in cui il messaggio sia stato
    ricevuto con successo;
- La connessione TCP si conclude.

Questo `e lo schema classico dell’Http/1.0 ovvero la prima forma di Http in cui il protocollo `e
stateless, ovvero che lo stato della connessione non `e salvato, e quindi non viene immagazzinato
nessun dato relativo all’utente, e connectionless ovvero che una volta eseguita una richiesta e
ricevuta la risposta la connessione TCP viene chiusa. Il protocollo `e presente in diverse versioni,
la pi`u diffusa `e la Http/1.1,^19 anche se dal 2015 `e apparsa la Http/2.0, la differenza principale

(^19) HTTP - Overview.url:https://www.tutorialspoint.com/http/http_overview.htm.


dalla prima `e che sono connection oriented ovvero che `e possibile inviare pi`u richieste e ricevere
pi`u risposte prima che la connessione venga definitivamente chiusa.

### 8.4 Struttura richiesta e risposta

Ogni volta che viene fatta una richiesta o viene fornita una risposta la struttura di queste non
cambia, differisce solo il contenuto:

Il messaggio HTTP inizia con una start-line che indica il metodo, il file e la versione del
protocollo; Successivamente c’`e l’header che comprende i dati che indicano il tipo del documento,
il browser, la data e altre informazioni generali, e infine c’`e il body che `e opzionale e conterr`a i
dati della risposta, o eventuali dati di una richiesta, per esempio con il metodo POST.

### 8.5 Tipo di richiesta

Esistono numerosi tipi di richieste Http, ognuna con il suo header e ognuna con il suo body:

- GET - Richiede una risorsa al server. Il body della richiesta `e vuoto o non presente, mentre
    nella risposta viene riempito con la risorsa indicata dall’URL o dall’URI inserito nell’header
    della richiesta.
- POST - Il metodo invia i dati al server. Il tipo del corpo della richiesta `e indicato
    dall’intestazione Content-Type.
- HEAD - Il metodo HEAD richiede una risposta identica a quella di una richiesta GET, ma
    senza il corpo della risposta.
- PUT - Il metodo PUT `e simile ad un POST quando la risorsa non `e presente, e quindi la
    crea; Se essa `e presente, la modifica. PUT ripetuti uguali non avranno effetti aggiuntivi, al
    contrario del POST che genererebbe pi`u risorse identiche.
- DELETE - Elimina la risorsa specificata.
- CONNECT - Richiede al server un accesso diretto al protocollo TCP.
- OPTIONS - Richiede al server l’elenco dei metodi validi per la risorsa specificata.
- TRACE - Esegue un ping al server, il quale risponde con le informazioni inviate.


### 8.6 Codici

Quando viene fatta una richiesta a seconda del risultato viene restituito un codice d’errore.

```
Formato del codice di stato Categoria
1XX(100,101,...) Codici di informazione
```
```
2XX(200,201,...)
Codici di successo, confermano al client che la richiesta
al server `e stata eseguita correttamente
3XX(300,301,...)
Ridirezione, codici che comunicano al client che la richiesta
avanzata non `e stata eseguita causa una risorsa mancante o spostata
4XX(400,401,...) Errori del client, la richiesta non `e stata eseguita perch`e errata
```
```
5XX(500,501,...)
Errori del server, la richiesta non `e stata eseguita per colpa di un
errore del server
```
```
Alcuni dei codici di stato pi`u diffusi e pi`u presenti sono:
```
```
Codice di stato Spiegazione
200 OK Richiesta eseguita correttamente
201 CREATED Risorsa creata con successo
202 ACCEPTED Richiesta accettata, ma non ancora eseguita
204 NO CONTENT Richiesta eseguita, nessun contenuto restituito
301 MOVED PERMANENTLY Risorsa esistente ma identificata diversamente
400 BAD REQEUST Richiesta errata
401 UNAUTHORIZED Richeista non autorizzata per mancata autenticazione
403 FORBIDDEN Richiesta proibita dai resquisiti di sicurezza del server
404 NOT FOUND Risorsa non trovata
500 INTERNAL SERVER ERROR Errore generico del server
501 NOT IMPLEMENTED Metodo non implementato
```
## 9 La sicurezza nel protocollo HTTP, (HTTPS)

### 9.1 Idea di fondo

Il normale traffico che avviene tramite il protocollo HTTP non `e in nessun modo protetto, infatti
l’idea di base dell’http `e solo quella di fornire una facile via di comunicazione; Negli anni successivi
al suo sviluppo, un maggior numero di persone connesse ha portato anche ad un incremento dei
rischi, uno dei problemi pi`u gravi era l’uso di packet sniffer, ovvero programmi che consentono
di catturare i pacchetti prelevandoli dalla rete, ci`o rendeva totalmente visibili le informazioni
che viaggiavano da un nodo all’altro al malintenzionato mettendo a rischio le informazioni di
tutti. La soluzione venne realizzata con la definizione di HTTPS descritto nella RFC 2965. Le
differenze tra HTTP e HTTPS non sono moltissime, anzi. Come si evince anche dal nome dei
due protocolli, l’unica reale divergenza sta nella maggior sicurezza dei dati e delle informazioni
personali che il secondo fornisce rispetto al primo. Si tratta, di fatto, dello stesso medesimo
protocollo di comunicazione web nel quale `e stato implementato il protocollo TLS, che garantisce
la crittografia delle informazioni e “l’identit`a” del portale web che si sta visitando.


### 9.2 SSL / TLS

Il Secure Socket Layer (SSL) `e il protocollo alla base del HTTPS, fornisce un servizio di sicurezza
al livello di trasporto del TCP, `e usato largamente dove `e necessario mantenere la privacy e
garantisce:

- Privatezza del collegamento: la riservatezza `e garantita mediante algoritmi di crittografia
    simmetrici come il DES (Cifratura a blocchi, chiavi da 64 bit);
- Autenticazione: l’autenticazione viene garantita con algoritmi a chiave pubblica come l’RSA
    (Rivest–Shamir–Adleman) , garantisce quindi che il client comunichi con il server corretto,
       introducendo a tale scopo anche meccanismi di certificazione.
- Affidabilit`a: sfruttando il MAC (Message Authentication Code) che utilizza funzioni Hash
    sicure come SHA (SHA-3) e MD5 (message digest) unite ad una chiave segreta per verificare
    l’integrit`a dei dati per evitare la manomissione durante la trasmissione.

```
SSL `e diffuso in molti sistemi:
```
- HTTPS: creato accoppiando SSL/TLS al protocollo HTTP standard, usa la porta 443 e
    https:// come prefisso.


- S-HTTP: `e poco diffuso e usa un apposito formato CMS ( Cryptographic Message Syntax)
    basato sul tls per crittografare i dati.
- SMTPS/POPS/IMAPS: Utilizzati per proteggere il contenuto delle email inviate.

```
Il protocollo SSL per rendere sicure le connessione esegue 4 fasi:
```
- Handshake: durante questa fase i l’utente che chiameremo Bob deve stabilire una connessione
    TCP con Alice, verificare la reale identit`a di quest’ultima e inviargli il MS. Una volta stabilit`a
    la connessione (SYN, TCP/SYNACK,TCP ACK) Bob invia un messaggio ad Alice la quale
    risponde con il suo CA Certificate. Ci`o garantisce l’identit`a di Alice allora Bob genera e
    spedisce il MS.
- Derivazione della chiave: dal MS si generano 2 encryption key usate per spedire i messaggi,
    inoltre verranno generati anche 2 MAC usati per verificare l’integrit`a di esso. Ovviamente
    si sarebbe potuto usare solo 1 chiave e 1 MAC ma `e considerato pi`u sicuro fornire chiavi
    diverse ad utenti diversi. Nella realt`a il procedimento risulta pi`u complesso e non si limita
    ad una divisione a 4 chiavi anzi, entra in gioco anche il PMS (Pre Master Secret). La scelta
    degli algoritmi viene fatta precedentemente durante l’Handshake, di solito si utilizza AES
    per la chiave simmetrica, RSA per quella pubblica e uno per il MAC.
- Trasferimento dati: ora che Bob e Alice condividono le 4 chiavi di sessione possono inviare i
    dati, per verificare l’integrit`a di ogni messaggio durante la trasmissione e non al termine, il
    protocollo SSL divide il flusso di dati in records, ovvero in un pacchetto con i campi tipo,
    versione, lunghezza, dati e MAC che permette quindi di poter garantire l’integrit`a dopo
    ogni singolo messaggio.
- Chiusura: Per chiudere la connessione TCP viene usato un record il cui campo ”tipo” `e
    impostato a SYN, che se ricevuto indica che la trasmissione `e conclusa e la linea viene
    chiusa.

Con il proseguire del tempo, il metodo SSL `e diventato sempre meno sicuro, e per ci`o `e stato
inventato il TLS. protocollo di livello 5 (sessione) che `e per`o composto da due livelli, il TLS
Record Protocol, che opera proprio al di sopra del TCP e funziona da base per la costruzione di
livelli superiori come l’Handshake Protocol; Il livello pi`u alto `e il TLS Handshake Protocol, si
occupa di autenticare l’interlocutore e di stabilire le chiavi segrete. Esistono numerose versioni del
TLS,^20 che vanno da TLS 1.0 alla pi`u recente TLS 1.3, quest’anno il TLS 1.1 e TLS 1.2 verranno
considerati deprecati.

## 10 Conclusione

Questo progetto ha cercato di rispondere alla domanda: “Quali strumenti esistono in epoca
moderna per determinare l’andamento di una epidemia?” A tal fine, `e stata condotta un’indagine
approfondita attraverso ricerche su internet, libri, siti web i quali sono stati citati sotto ogni pagina
dove essi comparivano. Dalle simulazioni con i dati reali `e emerso come per quanto semplice o
approssimativo il modello SIR scelto possa essere, il grafico generato rispecchia in parte la realt`a,
secondo una ricerca^21 del matematico McCluskey del 2008, i modelli SIR hanno una efficacia che
oscilla tra il 39% e il 61%, dovuto principalmente alle manovre di contenimento spesso attutate

(^20) Transport Layer Security. 2020.url:https://it.wikipedia.org/wiki/Transport_Layer_Security.
(^21) C. Connell McCluskey.Complete global stability for an SIR epidemic model with delay - Distributed or discrete.
2008.url:https://www.sciencedirect.com/science/article/pii/S1468121808002447.


nella seconda fase. Tuttavia, `e importante tenere presente che questa ricerca si `e concentrata
esclusivamente sui caratteri epidemiologici che influiscono sul comportamento del gruppo target.
Una volta messi a fuoco anche i fattori economici, geopolitici, psicologici e sopratutto sociali che
possono entrare in gioco, i risultati potrebbero variare. Per questo motivo, nessuna dichiarazione
generale o assolutamente oggettiva pu`o essere fatta circa i comportamenti messi in atto dalle
intere generazioni, non `e possibile elaborare una teoria unica valida per ogni caso possibile.

### 10.1 Futuri miglioramenti

Molte scelte nella realizzazione dell’APP e di questo documento sono state dettate sia dal tempo
che dalla lunghezza, infatti questo scritto cerca di riassumere in maniera ”breve” un progetto che
tratta temi molto complessi e lunghi da presentare, anche le parti di codice o le immagini esposte
sono solo una porzione di tutto ci`o che `e stato realizzato; Inoltre per questioni di tempo si sono
fatte scelte che hanno ovviamente ridotto l’efficacia delle valutazioni fatte, o la reale utilit`a di
esse. I possibili miglioramenti futuri potrebbero essere:

- La scelta del modello SIR, `e ovviamente molto restrittiva, esistono numerosi altri modelli
    decisamente pi`u complessi e pi`u lunghi da implementare che sarebbero molto pi`u precisi
    e che tengono in considerazione moltissimi altri fattori, come Mukwano,^22 un modello
    utilizzato per descrivere l’HIV con ben 22 input e 18 output.
- Un altro punto da implementare in futuro sar`a sicuramente aggiungere maggiori statistiche
    alla pagina statistiche.
- Modificare graficamente la pagina, per donargli uno stile ”proprio” e non definito dalla
    scelta di Blazor.
- Ottimizzare gli algoritmi scelti, per cercare di velocizzare al meglio i calcoli, ci`o `e dovuto
    principalmente alla scelta di Blazor WASM.
- Sviluppare un intelligenza artificiale propria, e senza l’utilizzo di un Model Builder, sfor-
    tunatamente il tempo necessario sarebbe stato eccessivo e quindi si `e voluto utilizzare lo
    strumento messo a disposizione da Microsoft.
- Realizzare un ambiente grafico per mostrare l’interazione tra i vari individui della popolazione,
    come realizzato da Primer^23 e da 3Blue1Brown,^24 utenti di youtube che si occupano
    principalmente di matematica e simulazioni.
- Trasportare pi`u codice in vari componenti di tipo generico.

(^22) I Andrianakis et al.History matching of a complex epidemiological model of human immunodeficiency virus
transmission by using variance emulation. 2017. url: https://www.ncbi.nlm.nih.gov/pmc/articles/
PMC5516248/.
(^23) Epidemic, Endemic, and Eradication Simulations.url:https://www.youtube.com/watch?v=7OLpKqTriio.
(^24) 3Blue1Brown.Simulating an epidemic. 2020.url:https://www.youtube.com/watch?v=gxAaO2rsdIs.


