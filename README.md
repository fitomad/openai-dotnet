# OpenAI .NET library

Fitomad.OpenAI is a .NET library that allows you to access the powerful AI models from OpenAI, such as GPT, DALL-E, and Whisper, through a simple and intuitive interface. You can use this framework to generate text, code, images, audio, and more, with just a few lines of code. 

Fitomad.OpenAI provides various options to customize your requests and responses. Whether you want to create a chatbot, a content generator, a sentiment analyzer, a translator, or any other AI-powered application, Fitomad.OpenAI can help you achieve your goals with ease and efficiency.

Currently I bring support for the following OpenAI models:

- Chat Completion
    - Text
    - Image explanation
- Image
- Audio
    - Create speech
    - Translation
    - Transcription
- Moderation

## OpenAI API key storage recommendations

### User secrets

```cs
var configuration = new ConfigurationBuilder()
    .AddUserSecrets<ImageTests >()
    .Build();

_apiKey = configuration.GetValue<string>("OpenAI:ApiKey");
```

### Environment variables

Environment variables are used to avoid storage of app secrets in code or in local configuration files. Environment variables override configuration values for all previously specified configuration sources.


## Dependency Injection

This is an example of DI in an Unit Testing (xunit) environment.
```cs
var aiSettings = new OpenAISettingsBuilder()
    .WithApiKey(_apiKey)
    .Build();

var services = new ServiceCollection();
services.AddOpenAIHttpClient(settings: aiSettings);
```

Below this lines you will find an example of the usage of DI in ASP.NET

```cs
using Fitomad.OpenAI

...

var openAISettings = new OpenAISettingsBuilder()
    .WithApiKey("sk...987")
    .Build();

builder.Services.AddOpenAIHttpClient(settings: openAISettings);
```

## Chat Completion

```cs
using Fitomad.OpenAI;
using Fitomad.OpenAI.Entities.Chat;
using Fitomad.OpenAI.Endpoints.Chat;

ChatRequest request = new ChatRequestBuilder()
    .WithModel(ChatModelKind.GPT_3_5_TURBO)
    .WithSystemMessage("Eres un profesor de alumnos de 10 años.")
    .WithUserMessage("Explícame qué es una estrella.")
    .WithTemperatute(TemperatureKind.Precise)
    .WithReponseFormat(ChatResponseFormat.Text)
    .Build();

ChatResponse chatResponse = await client.ChatCompletion.CreateChatAsync(request);
```

## Image

```cs
ImageRequest request = new ImageRequestBuilder()
    .WithModel(ImageModelKind.DALL_E_3)
    .WithPrompt("Un paisaje urbano, con algunos rascacielos de fondo aplicando un estilo de Dalí.")
    .WithImagesCount(1)
    .WithSize(DallE3Size.Square)
    .WithQuality(DallE3Quality.HD)
    .WithStyle(DallE3Style.Vivid)
    .WithResponseFormat(ImageResponseFormat.Url)
    .Build();

ImageResponse imageResponse = await client.Image.CreateImageAsync(request);
```

## Changes

### 0.2.1

- Enumeration `TemperatureKind` now is `Temperature` and has been moved to `Fitomad.OpenAI.Endpoints` namespace.
- Enumeration `ImageModelKind` now is `ImageModelType`
- Enumeration `ChatModelKind` now is `ChatModelType`
- Method `AddOpenAIHttpClient` is now in `Fitomad.OpenAI` namespace.