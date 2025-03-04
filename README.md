<picture>
 <source media="(prefers-color-scheme: dark)" srcset="docs/images/logo_dark.svg">
 <img alt="light" src="docs/images/logo_light.svg">
</picture>

This package provides access to [dxFeed market data](https://dxfeed.com/market-data/).
The library is built as a language-specific wrapper over
the [dxFeed Graal Native](https://dxfeed.jfrog.io/artifactory/maven-open/com/dxfeed/graal-native-api/) library,
which was compiled with [GraalVM Native Image](https://www.graalvm.org/latest/reference-manual/native-image/)
and [dxFeed Java API](https://docs.dxfeed.com/dxfeed/api/overview-summary.html) (our flagman API).

:information_source: If you already use [dxFeed .NET API](https://github.com/dxFeed/dxfeed-net-api), please see
the [Overview](#overview) section.<br>

![Build](https://github.com/dxFeed/dxfeed-graal-net-api/actions/workflows/build.yml/badge.svg)
![CodeQL](https://github.com/dxFeed/dxfeed-graal-net-api/actions/workflows/codeql.yml/badge.svg)
![Platform](https://img.shields.io/badge/platform-win--x64%20%7C%20linux--x64%20%7C%20osx--x64%20%7C%20osx--arm64-lightgrey)
[![NET](https://img.shields.io/badge/.NET_version-net6.0%20%7C%20net7.0%20%7C%20net8.0-blueviolet)](https://dotnet.microsoft.com/en-us/)
[![Release](https://img.shields.io/github/v/release/dxFeed/dxfeed-graal-net-api)](https://github.com/dxFeed/dxfeed-graal-net-api/releases/latest)
[![Nuget](https://img.shields.io/badge/nuget-1.1.0-blue)](https://dxfeed.jfrog.io/artifactory/nuget-open/com/dxfeed/graal-net/)
[![License](https://img.shields.io/badge/license-MPL--2.0-orange)](https://github.com/dxFeed/dxfeed-graal-net-api/blob/master/LICENSE)

## Table of Contents

- [Overview](#overview)
    * [Reasons for the New .NET API Repository](#reasons-for-the-new-net-api-repository)
    * [Benefits of the New Version](#benefits-of-the-new-version)
    * [Milestones](#milestones)
    * [Future Development](#future-development)
    * [Migration](#migration)
    * [Implementation Details](#implementation-details)
    * [Architectural Restrictions and Other Limitations in the Old Version](#architectural-restrictions-and-other-limitations-of-the-old-version)
- [Documentation](#documentation)
- [Requirements](#requirements)
- [Installation](#installation)
- [Usage](#usage)
    * [How to connect to QD endpoint](#how-to-connect-to-qd-endpoint)
    * [How to connect to dxLink](#how-to-connect-to-dxlink)
- [Tools](#tools)
- [Samples](#samples)
- [Current State](#current-state)
- [Dependencies](DEPENDENCIES.md)
- [3rd Party Licenses](THIRD_PARTY_LICENSES.md)

## Overview

### Reasons for the New .NET API Repository

The [old version](https://github.com/dxFeed/dxfeed-net-api) of dxFeed .NET API is built as a thin wrapper
over [dxFeed C API](https://github.com/dxFeed/dxfeed-c-api),
which has several [architectural restrictions](#architectural-restrictions-and-other-limitations-of-the-old-version)
that prevent us from providing a state-of-the-art technological solution.

### Benefits of the New Version

- :rocket: Increased performance
- :milky_way: Wider functionality
- :gemini: Identical programming interfaces to our best API
- :thumbsup: Higher quality of support and service

### Milestones

Feature development has already stopped for the [old version](https://github.com/dxFeed/dxfeed-net-api) of dxFeed .NET
API.

We expect the new repository to go into production in Q1’2024.
At the same time, the old version will be considered deprecated, and at the end of 2024, we plan to end the service.
If you’re already our customer and have difficulty with a future transition, please contact us via
our [customer portal](https://jira.in.devexperts.com/servicedesk/customer/portal/1).

### Future Development

Features planned with **high priority**:

* Add unit tests and conduct different types of testing
* Add necessary entities for more convenient API
  usage ([IPF](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/ipf/InstrumentProfile.html), [TimeSeriesEventModel](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/model/TimeSeriesEventModel.html), [OrderBookModel](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/model/market/OrderBookModel.html), [GetTimeSeriesPromise](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getTimeSeriesPromise-java.lang.Class-java.lang.Object-long-long-),
  etc.)
* Provide more samples
* Provide performance test results along with a comparison with the old API version

---
Features planned for the **next stage**:

* Implement a model
  of [incremental updates](https://kb.dxfeed.com/en/data-services/real-time-data-services/-net-api-incremental-updates.html)
  in Java API and add it to .NET API
* Implement OrderBookModel with advanced logic (e.g., OnNewBook, OnBookUpdate, OnBookIncrementalChange) in Java API and
  add it to .NET API
* Add samples or implement a convenient API
  for [Candlewebservice](https://kb.dxfeed.com/en/data-services/aggregated-data-services/candlewebservice.html)

### Migration

To help you rewrite the existing API calls, we’ve prepared [samples](#samples) demonstrating how to work with the new
API and how several functionalities are implemented. More examples will follow. The table below shows the sample mapping
between the old and new versions.

Our support team on our [customer portal](https://jira.in.devexperts.com/servicedesk/customer/portal/1/create/122) is
ready to answer any questions and help with the transition.

#### Sample Mapping

| # | Sample                                                                                                                            | Old Version                                                                                                                           | New Version                                                                                                 |
|:-:|:----------------------------------------------------------------------------------------------------------------------------------|:--------------------------------------------------------------------------------------------------------------------------------------|:------------------------------------------------------------------------------------------------------------|
| 1 | How to get Instrument Profiles                                                                                                    | [dxf_ipf_connect_sample](https://github.com/dxFeed/dxfeed-net-api/tree/master/samples/dxf_ipf_connect_sample)                         | [DxFeedIpfConnect](https://github.com/dxFeed/dxfeed-graal-net-api/tree/main/samples/DxFeedIpfConnect)       |
| 2 | How to get live updates for Instrument Profiles                                                                                   | [dxf_instrument_profile_live_sample](https://github.com/dxFeed/dxfeed-net-api/tree/master/samples/dxf_instrument_profile_live_sample) | [DxFeedLiveIpfSample](https://github.com/dxFeed/dxfeed-graal-net-api/tree/main/samples/DxFeedLiveIpfSample) |
| 3 | How to subscribe to `Order`, `SpreadOrder`, `Candle`, `TimeAndSale`, `Greeks`, `Series` snapshots                                 | [dxf_snapshot_sample](https://github.com/dxFeed/dxfeed-net-api/tree/master/samples/dxf_snapshot_sample)                               | *Q2’2024*, please see [TBD](#future-development) section                                                    |
| 4 | How to subscribe to depth of market                                                                                               | [dxf_price_level_book_sample](https://github.com/dxFeed/dxfeed-net-api/tree/master/samples/dxf_price_level_book_sample)               | *Q2’2024*, please see [TBD](#future-development) section                                                    |
| 5 | How to receive snapshots of `TimeAndSale`, `Candle`, `Series`, `Greeks` events on a given time interval without live subscription | [dxf_simple_data_retrieving_sample](https://github.com/dxFeed/dxfeed-net-api/tree/master/samples/dxf_simple_data_retrieving_sample)   | *Q2’2024*, please see [TBD](#future-development) section                                                    |
| 6 | How to subscribe to order snapshot with incremental updates                                                                       | [dxf_inc_order_snapshot_sample](https://github.com/dxFeed/dxfeed-net-api/tree/master/samples/dxf_inc_order_snapshot_sample)           | *Q2’2024*, please see [TBD](#future-development) section                                                    |
| 7 | How to retrieve `Candle` data from the candle web service                                                                         | [dxf_candle_data_retrieving_sample](https://github.com/dxFeed/dxfeed-net-api/tree/master/samples/dxf_candle_data_retrieving_sample)   | *Q4’2024*, please see [TBD](#future-development) section                                                    |
| 8 | How to retrieve `TimeAndSale` data from the candle web service                                                                    | [dxf_tns_data_retrieving_sample](https://github.com/dxFeed/dxfeed-net-api/tree/master/samples/dxf_tns_data_retrieving_sample)         | *Q4’2024*, please see [TBD](#future-development) section                                                    |

### Implementation Details

We use [GraalVM Native Image](https://www.graalvm.org/latest/reference-manual/native-image/) technology and specially
written code that *wraps* Java methods into native ones
to get dynamically linked libraries for different platforms (Linux, macOS, and Windows) based on
the [latest Java API package](https://dxfeed.jfrog.io/artifactory/maven-open/com/devexperts/qd/dxfeed-api/).

Then, the resulting dynamic link library (dxFeed Graal-native) is used through
C [ABI](https://en.wikipedia.org/wiki/Application_binary_interface) (application binary interface),
and we write programming interfaces that describe our business model (similar to Java API).

As a result, we get a full-featured, similar performance as with Java API.
Regardless of the language, writing the final application logic using API calls will be very similar (only the syntax
will be amended, *"best practices"*, specific language restrictions).

Below is a scheme of this process:

<picture>
 <source media="(prefers-color-scheme: dark)" srcset="docs/images/scheme_dark.svg">
 <img alt="light" src="docs/images/scheme_light.svg">
</picture>

### Architectural Restrictions and Other Limitations of the Old Version

| #  | Limitation                                                                                                                                                                                                                                                                                                                                   | How It’s Solved in the New Version                                                                                                                                                                                                                                                         |
|:--:|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 1  | Windows support only                                                                                                                                                                                                                                                                                                                         | Windows-x64, Linux-x64, macOS-x64, macOS-arm64 support by .NET                                                                                                                                                                                                                             |
| 2  | Single-threaded architecture limiting throughput                                                                                                                                                                                                                                                                                             | Based on the Java API, each subscription object ([DXFeedSubscription](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeedSubscription.html)) *can* run on its own thread                                                                                                              |
| 3  | User code in event callbacks (for example, [OnQuote](https://docs.dxfeed.com/net-api/classcom_1_1dxfeed_1_1api_1_1extras_1_1EventPrinter.html#a39bcd590edd9524b64b5fee00d56fccf)) is executed in the socket read thread, which can significantly reduce throughput                                                                           | Socket processing threads and callback threads are separated                                                                                                                                                                                                                               |
| 4  | In event callbacks, one market event type and one data portion always arrive (excluding snapshot subscription), which increases the load on the CPU with a large amount of incoming data                                                                                                                                                     | Event callbacks can receive different market event types, and more than one by batch                                                                                                                                                                                                       |
| 5  | It’s impossible to subscribe to data without getting [regionals](https://kb.dxfeed.com/en/data-model/exchange-codes.html) (if it is available for the market event) or only for a certain regional                                                                                                                                           | ```subscription.AddSymbols("AAPL");``` - [composite](https://kb.dxfeed.com/en/data-model/qd-model-of-market-events.html#quote-47603)<br>```subscription.AddSymbols("AAPL&Q");``` - [regional](https://kb.dxfeed.com/en/data-model/qd-model-of-market-events.html#quote-x--regional-quote-) |
| 6  | It’s impossible to subscribe to Order event (excluding snapshot subscription) without getting: all [sources](https://kb.dxfeed.com/en/data-model/qd-model-of-market-events.html#order-x), Order by Quote (including regionals), Order by [MarketMaker](https://kb.dxfeed.com/en/data-model/qd-model-of-market-events.html#marketmaker-47603) | ```subscription.AddSymbols(new IndexedEventSubscriptionSymbol("AAPL", OrderSource.NTV));``` - [Order.Source]() determines which data is being subscribed to                                                                                                                                |
| 7  | Data is mixed up when creating two subscriptions (regular and time series) for the same market event type. Both regular and time series data go to both subscriptions                                                                                                                                                                        | Each subscription instance receives only the data requested                                                                                                                                                                                                                                |
| 8  | Each subsequent request for the same symbol set in a subscription instance overwrites the existing one in another subscription instance                                                                                                                                                                                                      | Subscription instances and the data they receive are independent of each other                                                                                                                                                                                                             |
| 9  | Removing a symbol from one subscription instance caused it to be removed from all others                                                                                                                                                                                                                                                     | Subscription instances and the data they receive are independent of each other                                                                                                                                                                                                             |
| 10 | Incorrect behavior when reading from a file (if a market event in the file hasn’t been subscribed to). Reading from a file always occurs at maximum speed. The supported format is binary only                                                                                                                                               | ```endpoint.Connect(@"file:tape.txt[format=text]");``` - processing a text file with at it's "real" speed by timestamps<br>```endpoint.Connect(@"file:tape.bin[format=binary,speed=max]");``` - processing a binary file with max speed                                                    |

## Documentation

Find useful information in our self-service dxFeed Knowledge Base or .NET API documentation:

- [dxFeed Graal .NET API documentation](https://dxfeed.github.io/dxfeed-graal-net-api/)
- [dxFeed Knowledge Base](https://kb.dxfeed.com/index.html?lang=en)
    * [Getting Started](https://kb.dxfeed.com/en/getting-started.html)
    * [Troubleshooting](https://kb.dxfeed.com/en/troubleshooting-guidelines.html)
    * [Market Events](https://kb.dxfeed.com/en/data-model/dxfeed-api-market-events.html)
    * [Event Delivery contracts](https://kb.dxfeed.com/en/data-model/model-of-event-publishing.html#event-delivery-contracts)
    * [dxFeed API Event classes](https://kb.dxfeed.com/en/data-model/model-of-event-publishing.html#dxfeed-api-event-classes)
    * [Exchange Codes](https://kb.dxfeed.com/en/data-model/exchange-codes.html)
    * [Order Sources](https://kb.dxfeed.com/en/data-model/qd-model-of-market-events.html#order-x)
    * [Order Book reconstruction](https://kb.dxfeed.com/en/data-model/dxfeed-order-book/order-book-reconstruction.html)
    * [Symbology Guide](https://kb.dxfeed.com/en/data-model/symbology-guide.html)

## Requirements

### Windows

Only x64 versions are supported.

| OS                                    | Version        | Architectures |
|---------------------------------------|----------------|---------------|
| [Windows][Windows-client]             | 8, 8.1         | x64           |
| [Windows 10][Windows-client]          | Version 1607+  | x64           |
| [Windows 11][Windows-client]          | Version 22000+ | x64           |
| [Windows Server][Windows-Server]      | 2012+          | x64           |
| [Windows Server Core][Windows-Server] | 2012+          | x64           |
| [Nano Server][Nano-Server]            | Version 1809+  | x64           |

#### Requirements

* .NET (not required for self-contained assemblies)
* [Visual C++ Redistributable for Visual Studio 2015][vc_redist]

[Windows-client]: https://www.microsoft.com/windows/

[Windows-Server]: https://learn.microsoft.com/windows-server/

[Nano-Server]: https://learn.microsoft.com/windows-server/get-started/getting-started-with-nano-server

[vc_redist]: https://aka.ms/vs/17/release/vc_redist.x64.exe

### Linux

Only x64 versions are supported.

#### Requirements

* .NET (not required for self-contained assemblies)

#### Libc compatibility

- [glibc][glibc]: 2.35+ (from Ubuntu 22.04)
- [musl][musl]: temporarily unsupported

#### Libpthread compatibility

A symlink on libpthread.so, libpthread.so.0, or libcoreclr.so must exist.

[glibc]: https://www.gnu.org/software/libc/

[musl]: https://musl.libc.org/

### macOS

| OS             | Version | Architectures |
|----------------|---------|---------------|
| [macOS][macOS] | 10.15+  | x64           |
| [macOS][macOS] | 11+     | Arm64         |

Is supported in the Rosetta 2 x64 emulator.

[macOS]: https://support.apple.com/macos

#### Requirements

* .NET (not required for self-contained assemblies)

## Installation

Add this [package source](https://dxfeed.jfrog.io/artifactory/api/nuget/v3/nuget-open) to NuGet config.
<br/>
For example, you can create a [NuGet.Config](NuGet.Config) file in your solution folder with the following content:

```xml
<?xml version="1.0" encoding="utf-8"?>

<configuration>
    <packageSources>
        <add key="dxFeed" value="https://dxfeed.jfrog.io/artifactory/api/nuget/v3/nuget-open" protocolVersion="3"/>
    </packageSources>
</configuration>
```

Then add the *DxFeed.Graal.Net* package to your project using the NuGet package manager.

## Usage

### How to connect to QD endpoint

```csharp
using System;
using DxFeed.Graal.Net.Api;
using DxFeed.Graal.Net.Events.Market;

// For token-based authorization, use the following address format:
// "demo.dxfeed.com:7300[login=entitle:token]"
using var endpoint = DXEndpoint.Create().Connect("demo.dxfeed.com:7300");
using var subscription = endpoint.GetFeed().CreateSubscription(typeof(Quote));
subscription.AddEventListener(events =>
{
    foreach (var e in events)
    {
        Console.WriteLine(e);
    }
});
subscription.AddSymbols("AAPL");
Console.ReadKey();
```

<details>
<summary>Output</summary>
<br>

```
I 231130 141419.914 [main] QD - Using QDS-3.325+file-UNKNOWN, (C) Devexperts
I 231130 141419.925 [main] QD - Using scheme com.dxfeed.api.impl.DXFeedScheme slfwemJduh1J7ibvy9oo8DABTNhNALFQfw0KmE40CMI
I 231130 141419.934 [main] MARS - Started time synchronization tracker using multicast 239.192.51.45:5145 with SFmog
I 231130 141419.937 [main] MARS - Started JVM self-monitoring
I 231130 141419.938 [main] QD - qdnet with collectors [Ticker, Stream, History]
I 231130 141419.950 [main] ClientSocket-Distributor - Starting ClientSocketConnector to demo.dxfeed.com:7300
I 231130 141419.950 [demo.dxfeed.com:7300-Reader] ClientSocketConnector - Resolving IPs for demo.dxfeed.com
I 231130 141419.951 [demo.dxfeed.com:7300-Reader] ClientSocketConnector - Connecting to 208.93.103.170:7300
I 231130 141420.099 [demo.dxfeed.com:7300-Reader] ClientSocketConnector - Connected to 208.93.103.170:7300
D 231130 141420.246 [demo.dxfeed.com:7300-Reader] QD - Distributor received protocol descriptor multiplexor@fFLro [type=qtp, version=QDS-3.319, opt=hs, mars.root=mdd.demo-amazon.multiplexor-demo1] sending [TICKER, STREAM, HISTORY, DATA] from 208.93.103.170
Quote{AAPL, eventTime=0, time=20231130-135604.000+03:00, timeNanoPart=0, sequence=0, bidTime=20231130-135548+03:00, bidExchange=Q, bidPrice=189.43, bidSize=3, askTime=20231130-135604+03:00, askExchange=Q, askPrice=189.49, askSize=1}
```

</details>

### How to connect to dxLink

```csharp
using System;
using DxFeed.Graal.Net;
using DxFeed.Graal.Net.Api;
using DxFeed.Graal.Net.Events.Market;

// Enable experimental feature.
SystemProperty.SetProperty("dxfeed.experimental.dxlink.enable", "true");
// Set scheme for dxLink.
SystemProperty.SetProperty("scheme", "ext:opt:sysprops,resource:dxlink.xml");

// For token-based authorization, use the following address format:
// "dxlink:wss://demo.dxfeed.com/dxlink-ws[login=dxlink:token]"
using var endpoint = DXEndpoint.Create().Connect("dxlink:wss://demo.dxfeed.com/dxlink-ws");
using var subscription = endpoint.GetFeed().CreateSubscription(typeof(Quote));
subscription.AddEventListener(events =>
{
    foreach (var e in events)
    {
        Console.WriteLine(e);
    }
});
subscription.AddSymbols("AAPL");
Console.ReadKey();
```

<details>
<summary>Output</summary>
<br>

```
I 231130 141308.314 [main] QD - Using QDS-3.325+file-UNKNOWN, (C) Devexperts
I 231130 141308.326 [main] QD - Using scheme com.dxfeed.api.impl.DXFeedScheme slfwemJduh1J7ibvy9oo8DABTNhNALFQfw0KmE40CMI
I 231130 141308.351 [main] MARS - Started time synchronization tracker using multicast 239.192.51.45:5145 with DgKtZ
I 231130 141308.358 [main] MARS - Started JVM self-monitoring
I 231130 141308.359 [main] QD - qdnet with collectors [Ticker, Stream, History]
I 231130 141308.384 [main] DxLinkClientWebSocket-Distributor - Starting DxLinkClientWebSocketConnector to wss://demo.dxfeed.com/dxlink-ws
SLF4J: Failed to load class "org.slf4j.impl.StaticLoggerBinder".
SLF4J: Defaulting to no-operation (NOP) logger implementation
SLF4J: See http://www.slf4j.org/codes.html#StaticLoggerBinder for further details.
I 231130 141308.392 [wss://demo.dxfeed.com/dxlink-ws-Writer] DxLinkClientWebSocket-Distributor - Connecting to wss://demo.dxfeed.com/dxlink-ws
I 231130 141308.938 [wss://demo.dxfeed.com/dxlink-ws-Writer] DxLinkClientWebSocket-Distributor - Connected to wss://demo.dxfeed.com/dxlink-ws
D 231130 141310.105 [oioEventLoopGroup-2-1] QD - Distributor received protocol descriptor [type=dxlink, version=0.1-0.18-20231017-133150, keepaliveTimeout=120, acceptKeepaliveTimeout=5] sending [] from wss://demo.dxfeed.com/dxlink-ws
D 231130 141310.106 [oioEventLoopGroup-2-1] QD - Distributor received protocol descriptor [type=dxlink, version=0.1-0.18-20231017-133150, keepaliveTimeout=120, acceptKeepaliveTimeout=5, authentication=] sending [] from wss://demo.dxfeed.com/dxlink-ws
Quote{AAPL, eventTime=0, time=20231130-135604.000+03:00, timeNanoPart=0, sequence=0, bidTime=20231130-135548+03:00, bidExchange=Q, bidPrice=189.43, bidSize=3, askTime=20231130-135604+03:00, askExchange=Q, askPrice=189.49, askSize=1}
```

</details>

To familiarize with the dxLink protocol, please click [here](https://demo.dxfeed.com/dxlink-ws/debug/#/protocol).

## Tools

[Tools](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/src/DxFeed.Graal.Net.Tools/)
is a collection of utilities that allow you to subscribe to various market events for the specified symbols. The tools
can be downloaded from [Release](https://github.com/dxFeed/dxfeed-graal-net-api/releases)
(including self-contained versions, that do not require .NET installation):

* [Connect](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/src/DxFeed.Graal.Net.Tools/Connect/ConnectTool.cs)
  connects to the specified address(es) and subscribes to the specified events with the specified symbol
* [Dump](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/src/DxFeed.Graal.Net.Tools/Dump/DumpTool.cs)
  dumps all events received from address. This was designed to retrieve data from a file
* [PerfTest](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/src/DxFeed.Graal.Net.Tools/PerfTest/PerfTestTool.cs)
  connects to the specified address(es) and calculates performance counters (events per second, memory usage, CPU usage,
  etc.)
* [LatencyTest](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/src/DxFeed.Graal.Net.Tools/LatencyTest/LatencyTestTool.cs)
  connects to the specified address(es) and calculates latency
* [Qds](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/src/DxFeed.Graal.Net.Tools/Qds/QdsTool.cs)
  collection of tools ported from the Java qds-tools

To run tools on macOS, it may be necessary to unquarantine them:

```
sudo /usr/bin/xattr -r -d com.apple.quarantine <directory_with_tools>
```

## Samples

- [x] [ConvertTapeFile](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/samples/ConvertTapeFile/Program.cs)
  demonstrates how to convert one tape file to another tape file with optional intermediate processing or filtering
- [x] [DxFeedFileParser](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/samples/DxFeedFileParser/Program.cs)
  is a simple demonstration of how events are read form a tape file
- [x] [DxFeedSample](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/samples/DxFeedSample/Program.cs)
  is a simple demonstration of how to create multiple event listeners and subscribe to `Quote` and `Trade` events
- [x] [PrintQuoteEvents](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/samples/PrintQuoteEvents/Program.cs)
  is a simple demonstration of how to subscribe to the `Quote` event, using a `DxFeed` instance singleton
  and `dxfeed.properties` file
- [x] [WriteTapeFile](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/samples/WriteTapeFile/Program.cs)
  is a simple demonstration of how to write events to a tape file
- [x] [DxFeedIpfConnect](https://github.com/dxFeed/dxfeed-graal-net-api/tree/main/samples/DxFeedIpfConnect) is a simple
  demonstration of how to get Instrument Profiles
- [x] [DxFeedLiveIpfSample](https://github.com/dxFeed/dxfeed-graal-net-api/tree/main/samples/DxFeedLiveIpfSample) is a
  simple demonstration of how to get live updates for Instrument Profiles
- [ ] DxFeedPublishProfiles is a simple demonstration of how to publish market events
- [ ] ScheduleSample is a simple demonstration of how to get various scheduling information for instruments

## Current State

### Endpoint Roles

- [x] [FEED](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXEndpoint.Role.html#FEED)
  connects to the remote data feed provider and is optimized for real-time or delayed data processing,
  **this is a default role**
  ([.NET API sample](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/samples/DxFeedConnect/Program.cs))

- [x] [STREAM_FEED](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXEndpoint.Role.html#STREAM_FEED)
  is similar to `Feed` and also connects to the remote data feed provider but is designed for bulk data parsing from
  files
  ([.NET API sample](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/samples/DxFeedFileParser/Program.cs))

- [x] [PUBLISHER](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXEndpoint.Role.html#PUBLISHER)
  connects to the remote publisher hub (also known as multiplexor) or creates a publisher on the local host
  ([.NET API sample](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/samples/WriteTapeFile/Program.cs))

- [x] [STREAM_PUBLISHER](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXEndpoint.Role.html#STREAM_PUBLISHER)
  is similar to `Publisher` and also connects to the remote publisher hub, but is designed for bulk data publishing
  ([.NET API sample](https://github.com/dxFeed/dxfeed-graal-net-api/blob/main/samples/ConvertTapeFile/Program.cs))

- [x] [LOCAL_HUB](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXEndpoint.Role.html#LOCAL_HUB)
  is a local hub without the ability to establish network connections. Events published via `Publisher` are delivered to
  local `Feed` only

- [ ] [ON_DEMAND_FEED](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXEndpoint.Role.html#ON_DEMAND_FEED)
  is similar to `Feed`, but it is designed to be used
  with  [OnDemandService](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/ondemand/OnDemandService.html) for historical
  data replay only

### Event Types

- [x] [Order](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/Order.html)
  is a snapshot of the full available market depth for a symbol

- [x] [SpreadOrder](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/SpreadOrder.html)
  is a snapshot of the full available market depth for all spreads

- [x] [AnalyticOrder](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/AnalyticOrder.html)
  is an `Order` extension that introduces analytic information, such as adding iceberg-related information to a given
  order

- [x] [Trade](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/Trade.html)
  is a snapshot of the price and size of the last trade during regular trading hours and an overall day volume and day
  turnover

- [x] [TradeETH](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/TradeETH.html)
  is a snapshot of the price and size of the last trade during extended trading hours and the extended trading hours day
  volume and day turnover

- [x] [Candle](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/candle/Candle.html)
  event with open, high, low, and close prices and other information for a specific period

- [x] [Quote](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/Quote.html)
  is a snapshot of the best bid and ask prices and other fields that change with each quote

- [x] [Profile](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/Profile.html)
  is a snapshot that contains the security instrument description

- [x] [Summary](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/Summary.html)
  is a snapshot of the trading session, including session highs, lows, etc.

- [x] [TimeAndSale](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/TimeAndSale.html)
  represents a trade or other market event with price, such as the open/close price of a market, etc.

- [x] [Greeks](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/option/Greeks.html)
  is a snapshot of the option price, Black-Scholes volatility, and greeks

- [x] [Series](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/option/Series.html)
  is a snapshot of computed values available for all options series for a given underlying symbol based on options
  market prices

- [x] [TheoPrice](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/option/TheoPrice.html)
  is a snapshot of the theoretical option price computation that is periodically performed
  by [dxPrice](http://www.devexperts.com/en/products/price.html) model-free computation

- [x] [Underlying](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/option/Underlying.html)
  is a snapshot of computed values available for an option underlying symbol based on the market’s option prices

- [x] [OptionSale](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/market/OptionSale.html)
  represents a trade or another market event with the price (for example, market open/close price, etc.) for each option
  symbol listed under the specified `Underlying`

- [ ] [Configuration](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/misc/Configuration.html)
  is an event with an application-specific attachment

- [ ] [Message](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/misc/Message.html)
  is an event with an application-specific attachment

### Subscription Symbols

- [x] [String](https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-6.0)
  is a string representation of the symbol

- [x] [TimeSeriesSubscriptionSymbol](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/osub/TimeSeriesSubscriptionSymbol.html)
  represents subscription to time-series events

- [x] [IndexedEventSubscriptionSymbol](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/osub/IndexedEventSubscriptionSymbol.html)
  represents subscription to a specific source of indexed events

- [x] [WildcardSymbol.ALL](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/osub/WildcardSymbol.html)
  represents a  *wildcard* subscription to all events of the specific event type

- [x] [CandleSymbol](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/candle/CandleSymbol.html)
  is a symbol used with [DXFeedSubscription](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeedSubscription.html)
  class to subscribe for [Candle](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/event/candle/Candle.html) events

### Subscriptions & Models

- [x] [DXFeedSubscription](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeedSubscription.html)
  is a subscription for a set of symbols and event types

- [ ] [DXFeedTimeSeriesSubscription](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeedTimeSeriesSubscription.html)
  extends `DXFeedSubscription` to conveniently subscribe to time series events for a set of symbols and event types
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/api/DXFeedConnect.java))

- [ ] [ObservableSubscription](https://github.com/devexperts/QD/blob/master/dxfeed-api/src/main/java/com/dxfeed/api/osub/ObservableSubscription.java)
  is an observable set of subscription symbols for the specific event
  type ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/_simple_/PublishProfiles.java))

- [ ] [GetLastEvent](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getLastEvent-E-)
  returns the last event for the specified event instance
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/api/DXFeedSample.java))

- [ ] [GetLastEvents](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getLastEvents-java.util.Collection-)
  returns the last events for the specified event instances list

- [ ] [GetLastEventPromise](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getLastEventPromise-java.lang.Class-java.lang.Object-)
  requests the last event for the specified event type and symbol
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/console/LastEventsConsole.java))

- [ ] [GetLastEventsPromises](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getLastEventsPromises-java.lang.Class-java.util.Collection-)
  requests the last events for the specified event type and symbol collection

- [ ] [GetLastEventIfSubscribed](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getLastEventIfSubscribed-java.lang.Class-java.lang.Object-)
  returns the last event for the specified event type and symbol if there’s a subscription for it

- [ ] [GetIndexedEventsPromise](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getIndexedEventsPromise-java.lang.Class-java.lang.Object-com.dxfeed.event.IndexedEventSource-)
  requests an indexed events list for the specified event type, symbol, and source

- [ ] [GetIndexedEventsIfSubscribed](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getIndexedEventsIfSubscribed-java.lang.Class-java.lang.Object-com.dxfeed.event.IndexedEventSource-)
  returns a list of indexed events for the specified event type, symbol, and source, if there’s a subscription for it

- [ ] [GetTimeSeriesPromise](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getTimeSeriesPromise-java.lang.Class-java.lang.Object-long-long-)
  requests time series events for the specified event type, symbol, and time range
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/_simple_/FetchDailyCandles.java))

- [ ] [GetTimeSeriesIfSubscribed](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/api/DXFeed.html#getTimeSeriesIfSubscribed-java.lang.Class-java.lang.Object-long-long-)
  requests time series events for the specified event type, symbol, and time range if there’s a subscription for it

- [ ] [TimeSeriesEventModel](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/model/TimeSeriesEventModel.html)
  is a model of a list of time series events
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/ui/swing/DXFeedCandleChart.java))

- [ ] [IndexedEventModel](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/model/IndexedEventModel.html)
  is an indexed event list model
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/ui/swing/DXFeedTimeAndSales.java))

- [ ] [OrderBookModel](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/model/market/OrderBookModel.html)
  is a model of convenient Order Book management
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/ui/swing/DXFeedMarketDepth.java))

### IPF & Schedule

- [x] [InstrumentProfile](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/ipf/InstrumentProfile.html)
  represents basic profile information about a market instrument
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/ipf/DXFeedIpfConnect.java))

- [x] [InstrumentProfileReader](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/ipf/InstrumentProfileReader.html) reads
  instrument profiles from the stream using Instrument Profile Format (IPF)

- [x] [InstrumentProfileCollector](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/ipf/live/InstrumentProfileCollector.html)
  collects instrument profile updates and provides the live instrument profiles list
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/ipf/DXFeedLiveIpfSample.java))

- [ ] [InstrumentProfileConnection](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/ipf/live/InstrumentProfileConnection.html)
  connects to an instrument profile URL and reads instrument profiles with support of streaming live updates

- [ ] [Schedule](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/schedule/Schedule.html)
  provides an API to retrieving and exploring the trading schedules of various exchanges and different financial
  instrument classes
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/schedule/ScheduleSample.java))

- [ ] [Option Series](https://github.com/devexperts/QD/blob/master/dxfeed-api/src/main/java/com/dxfeed/ipf/option/OptionSeries.java)
  is a series of call and put options with different strike sharing the same attributes of expiration, last trading day,
  spc, multiplies,
  etc. ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/ipf/option/DXFeedOptionChain.java))

### Services

- [ ] [OnDemandService](https://docs.dxfeed.com/dxfeed/api/com/dxfeed/ondemand/OnDemandService.html)
  provides on-demand historical tick data replay controls
  ([Java API sample](https://github.com/devexperts/QD/blob/master/dxfeed-samples/src/main/java/com/dxfeed/sample/ondemand/OnDemandSample.java))
