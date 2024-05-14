# OpenAI .NET library

Fitomad.OpenAI is a **community-maintained .NET library** that allows you to access the powerful AI models from OpenAI, such as GPT, DALL-E, and Whisper, through a simple and intuitive interface. You can use this framework to generate text, code, images, audio, and more, with just a few lines of code. 

Fitomad.OpenAI provides various options to customize your requests and responses. Whether you want to create a chatbot, a content generator, a sentiment analyzer, a translator, or any other AI-powered application, Fitomad.OpenAI can help you achieve your goals with ease and efficiency.

The framework makes a heavy usage of the [Builder pattern](https://en.wikipedia.org/wiki/Builder_pattern) to create requests and settings objects.

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
- Models

## OpenAI API key storage recommendations

API key is a sensitive information part that must be keep safe during your development and deployment process.

I strongly recommend **the usage of environment variables** when you deploy your solition to store your OpenAI API key.

During the development stage you could use user-secrets technology to store the API key.

### User secrets

This is the recommended storage system for development. For a detailed information about the usage of this storage system, please refer to [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=linux) article.

```cs
var configuration = new ConfigurationBuilder()
    .AddUserSecrets<ImageTests >()
    .Build();

_apiKey = configuration.GetValue<string>("OpenAI:ApiKey");
```

### Environment variables

Environment variables are used to avoid storage of app secrets in code or in local configuration files. Environment variables override configuration values for all previously specified configuration sources.

```cs
using Fitomad.OpenAI;

...

var openAISettings = new OpenAISettingsBuilder()
    .WithApWithApiKeyFromEnvironmentVariableiKey("OpenAI:ApiKey")
    .Build();
```

## Dependency Injection. Create an `OpenAIClient` instance

To create a `OpenAIClient` instance, the entry point to the whole Fitomad.OpenAI framework, developers must use DI.

I provide a helper method registered as an `IServiceCollection` extension named `AddOpenAIHttpClient` which receives an `OpenAISettings` object as parameter.

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
using Fitomad.OpenAI;

...

var developApiKey = builder.Configuration["OpenAI:ApiKey"];

var openAISettings = new OpenAISettingsBuilder()
    .WithApiKey(developApiKey)
    .Build();

builder.Services.AddOpenAIHttpClient(settings: openAISettings);
```

And now, thanks to the built-on DI container available in .NET we can use the `OpenAIClient` registered type 

```cs
...

[ApiController]
[Route("games")]
public class GameController: ControllerBase
{
    private IOpenAIClient _openAIClient;

    public GameController(IOpenAIClient openAIClient)
    {
        _openAIClient = openAIClient;
    }

    ...
}
```

## Chat Completion

Maybe, the best known endpoint available in the API, Fitomad.OpenAI framework allows developers to invoke to different operations

- Chats
- Image content explanation

### Chat

Here's an example of a chat completion where developer set de *mood* to shool teacher and ask about what is and star (in Spanish 🇪🇸😜)
```cs
using Fitomad.OpenAI;
using Fitomad.OpenAI.Entities.Chat;
using Fitomad.OpenAI.Endpoints.Chat;

ChatRequest request = new ChatRequestBuilder()
    .WithModel(ChatModelType.GPT_3_5_TURBO)
    .WithSystemMessage("Eres un profesor de alumnos de 10 años.")
    .WithUserMessage("Explícame qué es una estrella.")
    .WithTemperatute(Temperature.Precise)
    .WithReponseFormat(ChatResponseFormat.Text)
    .Build();

ChatResponse chatResponse = await client.ChatCompletion.CreateChatAsync(request);
```
The GPT model answer is available in the `Choices` property, that is a `Choice type` that stores the messages in the property `ReceivedMessage`, a `Message` record type.

### Image Explanation

No need of builder object to create the request, simply pass the image url and user question to method and done!.

```cs
var imageUrl = "https://upload.wikimedia.org/wikipedia/commons/a/ae/Vel%C3%A1zquez_-_La_Fragua_de_Vulcano_%28Museo_del_Prado%2C_1630%29.jpg"; 
var question = "¿Qué cuadro es este?";

var imageExplanationResponse = await _client.ChatCompletion.ExplainImageAsync(imageUrl, userQuestion: question);
```

In the example above I ask GTP to exaplain the image "La Fragua de Vulcano" by Diego de Velázquez available in the Museo Nacional del Prado.

The response from GTP model must be treated in the same way as I describe in the *Chat* section.

## Image

```cs
ImageRequest request = new ImageRequestBuilder()
    .WithModel(ImageModelKind.DALL_E_3)
    .WithPrompt("Un paisaje urbano, con algunos rascacielos de fondo aplicando el estilo de Dalí.")
    .WithImagesCount(1)
    .WithSize(DallE3Size.Square)
    .WithQuality(DallE3Quality.HD)
    .WithStyle(DallE3Style.Vivid)
    .WithResponseFormat(ImageResponseFormat.Url)
    .Build();

ImageResponse imageResponse = await client.Image.CreateImageAsync(request);
```

The images created by DALL-E are available in the `Images` property of the `ImageResponse` record. The `Images` is an array of `ImageUrl`.

## Audio

Support the *create speech*, *translation* and *transcription* operations.

### Speech

```cs
private const string ElQuijote = "En un lugar de la Mancha, de cuyo nombre no quiero acordarme, no ha mucho tiempo que vivía un hidalgo de los de lanza en astillero, adarga antigua, rocín flaco y galgo corredor. Una olla de algo más vaca que carnero, salpicón las más noches, duelos y quebrantos los sábados, lantejas los viernes, algún palomino de añadidura los domingos, consumían las tres partes de su hacienda.";

SpeechRequest request = new SpeechRequestBuilder()
    .WithModel(SpeechModelType.TTS_1)
    .WithVoice(VoiceType.Onyx)
    .WithResponseFormat(SpeechResponseFormat.MP3)
    .WithInput(ElQuijote)
    .Build();

SpeechResponse response = await _client.Audio.CreateSpeech(request);
```

### Transcription

```cs
TranscriptionRequest request = new TranscriptionRequestBuilder()
    .WithModel(TranscriptionModelType.Whisper1)
    .WithResponseFormat(TranscriptionResponseFormat.Json)
    .WithFile("/path/to/audio-file.mp3")
    .Build();

TranscriptionResponse response = await _client.Audio.CreateTranscription(request);
```

The transcription will be stored in the `Text` property in the `TranscriptionResponse`.

### Translation

```cs
TranslationRequest request = new TranslationRequestBuilder()
    .WithModel(TranslationModelType.Whisper1)
    .WithResponseFormat(TranslationResponseFormat.Json)
    .WithFile("/path/to/audio-file.mp3")
    .Build();

TranslationResponse response = await _client.Audio.CreateTranslation(request);
```

The translation will be stored in the `Text` property in the `TranslationResponse`.

## Moderation

As OpenAI said, moderation represents policy compliance report by OpenAI's content moderation model against a given input.

```cs
const string ElBuscon = "Yo, señora, soy de Segovia. Mi padre se llamó Clemente Pablo, natural del mismo pueblo; Dios le tenga en el cielo. Fue, tal como todos dicen, de oficio barbero, aunque eran tan altos sus pensamientos que se corría de que le llamasen así, diciendo que él era tundidor de mejillas y sastre de barbas.";

var moderationRequest = new ModerationRequestBuilder()
    .WitnInput(ElBuscon)
    .WithModel(ModerationModelType.TextModerationLatest)
    .Build();

ModerationResponse response = await _client.Moderation.CreateModeration(moderationRequest);
```

You will check the results thanks two different properties named `Values` and `Scores`.

The `Values` property is a data structure with boolean properties that indicates if the text is *positive* in some of the moderated categories.

The `Scores` property is a data structure like `Values` but instead of booleand presents `double` properties that show the *score* in each moderated category for the given text.

## Models

Fetch a list of models available in the API. Fitomad.OpenAI framework bring support for *list*, *retreive* and *delete* operations.

This is one of the most simple endpoints, and you will not need a builder object to create a request, simply invoke the methods presented in the `ModelEndpoint` class.

List operation 

```cs
ModelListResponse response = await _client.Models.List();
```

Retrieve a given model.

```cs
ModelResponse response = await _client.Models.Retrieve(model: modelName);
```

Delete a model.

```cs
ModelDeletedResponse response = await _client.Models.Delete(model: modelName);
```

## Changes

### 1.0.2

- Chat endpoint models brings support the following:
    - `gpt-4o` 🚀
    - `gpt-4-turbo`
    - `gpt-4-turbo-2024-04-09`
    - `gpt-4-turbo-preview`

### 0.2.1

- New package icon 🎉
- Namespace `Fitomad.OpenAI.Models` now is `Fitomad.OpenAI.Endpoints`
- Enumeration `TemperatureKind` now is `Temperature` and has been moved to `Fitomad.OpenAI.Endpoints` namespace.
- Enumeration `ImageModelKind` now is `ImageModelType`
- Enumeration `ChatModelKind` now is `ChatModelType`
- Method `AddOpenAIHttpClient` is now in `Fitomad.OpenAI` namespace.