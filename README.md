<img src="https://i.ibb.co/93mSYZ4/decay.png" alt="decay.dev" width="256"/><img src="https://i.ibb.co/0yc10cV/drops.png" width="256">

# DecayDrops

Simple and efficient drop manager. Control flight times, drop times, and spawn drops on canisters. Designed to keep your server from lagging, 86 lines of code.

## Plugin Attributes

- Control the duration of cargo and supply drop flight times
- Control the duration of the drop time
- Spawn supply drops directly on canister
- Remove signal smoke once dropped
- does not attempt to generate any base config code
- does do not rely on or use Rust/Facepunch DLL's outside of what `oxide` uses
```
  using Newtonsoft.Json;
  using System;
  using System.Collections.Generic;
  using System.Linq;
```

## Install/Requirements
- rust/oxide server
- generate your `DecayDrops.json` config **FIRST** and move it to the `oxide/config/` directory in oxide
- move `DecayDrops.cs` into `oxide/plugins/` directory
- reload plugin

### DecayDrops JSON Schema
```json
{
  "flight_duration": 10,
  "drop_duration": 10
}
```
