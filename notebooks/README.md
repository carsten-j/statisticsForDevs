
Fordeling af svar tider fra websites

Motivation kunne fx være

--
Citat fra artikel om Amazon Dynamo

A common approach in the industry for forming a performance
oriented SLA is to describe it using average, median and expected
variance. At Amazon we have found that these metrics are not
good enough if the goal is to build a system where all customers
have a good experience,


http://www.allthingsdistributed.com/files/amazon-dynamo-sosp2007.pdf

The paper rejects the notion of defining SLAs with median expectations. Instead, Dynamo uses 99.9 percentiles. The central idea is that the SLA defines acceptable usage for all users - not just for half of them (the median).
-

Lidt mere generelle betragtning om fordelingen

Se fx
https://newrelic.com/blog/best-practices/expected-distributions-website-response-times

Bemærkning om at fordelingen (vist ved et histogram) kan have flere peaks
Se fx mere om "modes" her: https://www.brendangregg.com/frequencytrails.html
og videre her https://adrianco.medium.com/percentiles-dont-work-analyzing-the-distribution-of-response-times-for-web-services-ace36a6a2a19

Endvidere fra sidste link

Response times are a one sided distribution. There’s a minimum possible response time for when everything goes right and there’s no delays. Every opportunity for delay due to more work than the best case or more time waiting than the best case increases the latency and they all add up and create a long tail.

Check out  [hdrhistogram package](http://www.hdrhistogram.org/)
Build by Gil Tene
https://www.youtube.com/watch?v=lJ8ydIuPFeU

--

Responsiveness goal
https://developer.mozilla.org/en-US/docs/Web/Performance/How_long_is_too_long#responsiveness_goal


RED signals
https://grafana.com/files/grafanacon_eu_2018/Tom_Wilkie_GrafanaCon_EU_2018.pdf

---
Hvorfor det er bedre at bruge percentiler fremfor gennemsnit/median

https://orangematter.solarwinds.com/2016/11/18/why-percentiles-dont-work-the-way-you-think/

1.  Averages hide the outliers, so you can’t see them.
2.  Outliers skew averages, so in a system with outliers, the average doesn’t represent typical behavior.

As Amazon’s Werner Vogels said in an AWS re:Invent keynote, the only thing an average tells you is half of your customers are having a worse experience.

Vær opmærksom på hvordan TSDB gemmer metrics - da det f.eks ikke giver mening at tage gennemsnit af 95% kvartil.


If a percentile requires the population of original events—such as measurements of every web page load—we have a big problem. A Big Data problem, to be exact. Percentiles are notoriously expensive to compute because of this.
Lots of ways to compute _approximate_ percentiles are almost as good as keeping the entire population and querying and sorting it.

Fx bruger Prometheus histogrammer. Men der er også andre teknikker som fx reservoir sampling https://medium.com/paypal-tech/statistics-for-software-e395ca08005d

HVOR STOR EN FEJL INTRODUCERES VED AT BRUGE DISSE APPROKSIMATIONER?

Alternativt til histogrammer er banded metrics

Uanset om man vælger det ene eller andet så er

Choosing the ranges well is a hard problem, generally. Common solutions include logarithmic ranges and ranges providing a given number of significant digits but may be faster to calculate at the cost of not growing uniformly. Even divisions are rarely a good choice. For more on these topics, [please read Brendan Gregg’s excellent write-up](http://www.brendangregg.com/FrequencyTrails/modes.html).

Percentile er bedre end average men det er stadig kun et tal. For mere info kan man bruge heatmaps.
Se fx https://www.brendangregg.com/HeatMaps/latency.html#HeatMap


# Histogrammer i Promethus

Best practices
https://prometheus.io/docs/practices/histograms/

Error estimat i appraksimation

Apdex score

Ny feature på vej native histograms:
https://promcon.io/2022-munich/talks/native-histograms-in-prometheus/

Se mere på https://twitter.com/juliusvolz/status/1142183723179368448
og
https://promlabs.com/blog/2021/01/29/how-exactly-does-promql-calculate-rates/
for hvordan kvartiler bestemmes udfra histogram
Læs især spørgsmål stillet af mig nederst på siden.

Heatmaps i Grafana
https://grafana.com/blog/2020/06/23/how-to-visualize-prometheus-histograms-in-grafana/


## Referencer
https://www.brendangregg.com/systems-performance-2nd-edition-book.html


gennemsnit som balance punkt
[Duxbury Advanced] John A. Rice - Mathematical Statistics and Data Analysis 3ed (Duxbury Advanced)   (2006, Duxbury Press)
