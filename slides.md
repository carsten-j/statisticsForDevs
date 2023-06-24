---
theme: moon
transition: "slide"
highlightTheme: "monokai"
slideNumber: false
---

# Svartider, fraktiler og Prometheus

---

## Hvad er formålet med dagens præsentation?

--

Vi vil gerne forstå, hvordan Prometheus beregner fraktiler!

--

Men først tager vi en hurtig introduktion til histogrammer og fraktiler.

--

Og dernæst en kort snak om svartider og deres fordelinger.

---

## Histogrammer og fraktiler

DEMO med Python og Jupyter

---

## Fordeling af svartider fra websites

--

Det er (måske) velkendt, at svartider ikke er normalfordelt.

![svartider](https://newrelic.com/sites/default/files/wp_blog_inline_files/right-skewed-long-tail-distribution.png)

--

Det er heller ikke er ualmindeligt, at fordelingen har flere peaks. Det kunne fx
skyldes en cache, hvor første peak rammer cache, mens andet peak rammer databasen.

![multimodel](https://newrelic.com/sites/default/files/wp_blog_inline_files/multi-modal-histogram.png)

--

Hvorfor er svartider ikke normalfordelte?

--

Response times are a one sided distribution. There’s a minimum possible response time for when everything goes right and there’s no delays. Every opportunity for delay due to more work than the best case or more time waiting than the best case increases the latency and they all add up and create a long tail.

Kilde: Adrian Cockcroft https://adrianco.medium.com/percentiles-dont-work-analyzing-the-distribution-of-response-times-for-web-services-ace36a6a2a19

--

Der findes forskellige bud på en analytisk fordeling, der kan beskrive svartider, men det
kommer vi ikke til at se nærmere på her. I stedet tager vi en ikke-parametrisk tilgang til
beskrivelse af fordelingen via fraktiler.

--

Indtil starten af 2010 var det almindeligt at bruge gennemsnit og median til at beskrive
fordelingen.

--

Hvorfor er det generelt ikke en god ide?

--

Det ikke giver et retvisende billede af svartiderne, når fordelingen har en lang hale.

--

Som det humoristisk er beskrevet i [The Most Misleading Measure of Response Time: Average](https://www.optimizely.com/insights/blog/why-cdn-balancing/):

Because looking at your average response time is like measuring the average temperature of a hospital. What you really care about is a patient’s temperature, and in particular, the patients who need the most help. The best way to measure this is to track the 99th percentile response time: the worst 1%.

--

A common approach in the industry for forming a performance
oriented SLA is to describe it using average, median and expected
variance. At Amazon we have found that these metrics are not
good enough if the goal is to build a system where all customers
have a good experience,

Citat fra artikel om Amazon Dynamo: http://www.allthingsdistributed.com/files/amazon-dynamo-sosp2007.pdf

---

## Prometheus og fraktiler

--

Træk vejret dybt. Nu går vi virkelig i detaljen og undersøger, hvordan Prometheus beregner fraktiler.

--

DEMO
