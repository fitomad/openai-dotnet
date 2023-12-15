using Fitomad.OpenAI.Entities.Audio;
using Fitomad.OpenAI.Extensions;

namespace Fitomad.OpenAI.Endpoints.Audio;

using TranslationModelType = Fitomad.OpenAI.Endpoints.Audio.TranscriptionModelType;
using TranslationResponseFormat = Fitomad.OpenAI.Endpoints.Audio.TranscriptionResponseFormat;

public sealed class TranslationRequestBuilder
{
    private TranslationRequest _request = new TranslationRequest();

    public TranslationRequestBuilder WithFile(string path)
    {
        if(File.Exists(path))
        {
            byte[] content = File.ReadAllBytes(path);
            _request.File = content;
        } 
        else
        {
            throw new OpenAIException($"File at path {path} doesn't exists.");
        } 

        return this;
    }

    public TranslationRequestBuilder WithModel(TranslationModelType model)
    {
        return WithModel(model.GetValue());
    }

    public TranslationRequestBuilder WithModel(string modelName)
    {
        _request.Model = modelName;
        return this;
    }

    public TranslationRequestBuilder WithLanguage(string languageIsoCode)
    {
        _request.Language = languageIsoCode;
        return this;
    }

    public TranslationRequestBuilder WithPrompt(string prompt)
    {
        _request.Prompt = prompt;
        return this;
    }

    public TranslationRequestBuilder WithResponseFormat(TranslationResponseFormat format)
    {
        return WithResponseFormat(format.GetValue());
    }

    public TranslationRequestBuilder WithResponseFormat(string formatName)
    {
        _request.ResponseFormat = formatName;
        return this;
    }

    public TranslationRequestBuilder WithTemperature(Temperature temperature)
    {
        return WithTemperature(temperature.GetValue());
    }

    public TranslationRequestBuilder WithTemperature(double value)
    {
        _request.Temperatute = value;
        return this;
    }

    public TranslationRequest Build()
    {
        if(_request.File is null || _request.File.Length == 0)
        {
            throw new OpenAIException("A valid file content is mandatory for translation operations.");
        }

        if(string.IsNullOrEmpty(_request.Model))
        {
            throw new OpenAIException("You must indicates a valid model.");
        }

        if(_request.Temperatute.IsOutOfOpenAIRange())
        {
            throw new OpenAIException("Temperature value id out of valid range.");
        }

        return _request;
    }
}