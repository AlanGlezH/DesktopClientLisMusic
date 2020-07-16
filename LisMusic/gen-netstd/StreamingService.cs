/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


public partial class StreamingService
{
  public interface IAsync
  {
    Task<TrackAudio> GetTrackAudioAsync(TrackRequest trackRequest, CancellationToken cancellationToken = default(CancellationToken));

    Task<TrackUploaded> UploadTrackAsync(TrackAudio trackAudio, CancellationToken cancellationToken = default(CancellationToken));

  }


  public class Client : TBaseClient, IDisposable, IAsync
  {
    public Client(TProtocol protocol) : this(protocol, protocol)
    {
    }

    public Client(TProtocol inputProtocol, TProtocol outputProtocol) : base(inputProtocol, outputProtocol)    {
    }
    public async Task<TrackAudio> GetTrackAudioAsync(TrackRequest trackRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
      await OutputProtocol.WriteMessageBeginAsync(new TMessage("GetTrackAudio", TMessageType.Call, SeqId), cancellationToken);
      
      var args = new GetTrackAudioArgs();
      args.TrackRequest = trackRequest;
      
      await args.WriteAsync(OutputProtocol, cancellationToken);
      await OutputProtocol.WriteMessageEndAsync(cancellationToken);
      await OutputProtocol.Transport.FlushAsync(cancellationToken);
      
      var msg = await InputProtocol.ReadMessageBeginAsync(cancellationToken);
      if (msg.Type == TMessageType.Exception)
      {
        var x = await TApplicationException.ReadAsync(InputProtocol, cancellationToken);
        await InputProtocol.ReadMessageEndAsync(cancellationToken);
        throw x;
      }

      var result = new GetTrackAudioResult();
      await result.ReadAsync(InputProtocol, cancellationToken);
      await InputProtocol.ReadMessageEndAsync(cancellationToken);
      if (result.__isset.success)
      {
        return result.Success;
      }
      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "GetTrackAudio failed: unknown result");
    }

    public async Task<TrackUploaded> UploadTrackAsync(TrackAudio trackAudio, CancellationToken cancellationToken = default(CancellationToken))
    {
      await OutputProtocol.WriteMessageBeginAsync(new TMessage("UploadTrack", TMessageType.Call, SeqId), cancellationToken);
      
      var args = new UploadTrackArgs();
      args.TrackAudio = trackAudio;
      
      await args.WriteAsync(OutputProtocol, cancellationToken);
      await OutputProtocol.WriteMessageEndAsync(cancellationToken);
      await OutputProtocol.Transport.FlushAsync(cancellationToken);
      
      var msg = await InputProtocol.ReadMessageBeginAsync(cancellationToken);
      if (msg.Type == TMessageType.Exception)
      {
        var x = await TApplicationException.ReadAsync(InputProtocol, cancellationToken);
        await InputProtocol.ReadMessageEndAsync(cancellationToken);
        throw x;
      }

      var result = new UploadTrackResult();
      await result.ReadAsync(InputProtocol, cancellationToken);
      await InputProtocol.ReadMessageEndAsync(cancellationToken);
      if (result.__isset.success)
      {
        return result.Success;
      }
      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "UploadTrack failed: unknown result");
    }

  }

  public class AsyncProcessor : ITAsyncProcessor
  {
    private IAsync _iAsync;

    public AsyncProcessor(IAsync iAsync)
    {
      if (iAsync == null) throw new ArgumentNullException(nameof(iAsync));

      _iAsync = iAsync;
      processMap_["GetTrackAudio"] = GetTrackAudio_ProcessAsync;
      processMap_["UploadTrack"] = UploadTrack_ProcessAsync;
    }

    protected delegate Task ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken);
    protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

    public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot)
    {
      return await ProcessAsync(iprot, oprot, CancellationToken.None);
    }

    public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
    {
      try
      {
        var msg = await iprot.ReadMessageBeginAsync(cancellationToken);

        ProcessFunction fn;
        processMap_.TryGetValue(msg.Name, out fn);

        if (fn == null)
        {
          await TProtocolUtil.SkipAsync(iprot, TType.Struct, cancellationToken);
          await iprot.ReadMessageEndAsync(cancellationToken);
          var x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
          await oprot.WriteMessageBeginAsync(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID), cancellationToken);
          await x.WriteAsync(oprot, cancellationToken);
          await oprot.WriteMessageEndAsync(cancellationToken);
          await oprot.Transport.FlushAsync(cancellationToken);
          return true;
        }

        await fn(msg.SeqID, iprot, oprot, cancellationToken);

      }
      catch (IOException)
      {
        return false;
      }

      return true;
    }

    public async Task GetTrackAudio_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
    {
      var args = new GetTrackAudioArgs();
      await args.ReadAsync(iprot, cancellationToken);
      await iprot.ReadMessageEndAsync(cancellationToken);
      var result = new GetTrackAudioResult();
      try
      {
        result.Success = await _iAsync.GetTrackAudioAsync(args.TrackRequest, cancellationToken);
        await oprot.WriteMessageBeginAsync(new TMessage("GetTrackAudio", TMessageType.Reply, seqid), cancellationToken); 
        await result.WriteAsync(oprot, cancellationToken);
      }
      catch (TTransportException)
      {
        throw;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Error occurred in processor:");
        Console.Error.WriteLine(ex.ToString());
        var x = new TApplicationException(TApplicationException.ExceptionType.InternalError," Internal error.");
        await oprot.WriteMessageBeginAsync(new TMessage("GetTrackAudio", TMessageType.Exception, seqid), cancellationToken);
        await x.WriteAsync(oprot, cancellationToken);
      }
      await oprot.WriteMessageEndAsync(cancellationToken);
      await oprot.Transport.FlushAsync(cancellationToken);
    }

    public async Task UploadTrack_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
    {
      var args = new UploadTrackArgs();
      await args.ReadAsync(iprot, cancellationToken);
      await iprot.ReadMessageEndAsync(cancellationToken);
      var result = new UploadTrackResult();
      try
      {
        result.Success = await _iAsync.UploadTrackAsync(args.TrackAudio, cancellationToken);
        await oprot.WriteMessageBeginAsync(new TMessage("UploadTrack", TMessageType.Reply, seqid), cancellationToken); 
        await result.WriteAsync(oprot, cancellationToken);
      }
      catch (TTransportException)
      {
        throw;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Error occurred in processor:");
        Console.Error.WriteLine(ex.ToString());
        var x = new TApplicationException(TApplicationException.ExceptionType.InternalError," Internal error.");
        await oprot.WriteMessageBeginAsync(new TMessage("UploadTrack", TMessageType.Exception, seqid), cancellationToken);
        await x.WriteAsync(oprot, cancellationToken);
      }
      await oprot.WriteMessageEndAsync(cancellationToken);
      await oprot.Transport.FlushAsync(cancellationToken);
    }

  }


  public partial class GetTrackAudioArgs : TBase
  {
    private TrackRequest _trackRequest;

    public TrackRequest TrackRequest
    {
      get
      {
        return _trackRequest;
      }
      set
      {
        __isset.trackRequest = true;
        this._trackRequest = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool trackRequest;
    }

    public GetTrackAudioArgs()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.Struct)
              {
                TrackRequest = new TrackRequest();
                await TrackRequest.ReadAsync(iprot, cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("GetTrackAudio_args");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        if (TrackRequest != null && __isset.trackRequest)
        {
          field.Name = "trackRequest";
          field.Type = TType.Struct;
          field.ID = 1;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await TrackRequest.WriteAsync(oprot, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as GetTrackAudioArgs;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.trackRequest == other.__isset.trackRequest) && ((!__isset.trackRequest) || (System.Object.Equals(TrackRequest, other.TrackRequest))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.trackRequest)
          hashcode = (hashcode * 397) + TrackRequest.GetHashCode();
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("GetTrackAudio_args(");
      bool __first = true;
      if (TrackRequest != null && __isset.trackRequest)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("TrackRequest: ");
        sb.Append(TrackRequest== null ? "<null>" : TrackRequest.ToString());
      }
      sb.Append(")");
      return sb.ToString();
    }
  }


  public partial class GetTrackAudioResult : TBase
  {
    private TrackAudio _success;

    public TrackAudio Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool success;
    }

    public GetTrackAudioResult()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.Struct)
              {
                Success = new TrackAudio();
                await Success.ReadAsync(iprot, cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("GetTrackAudio_result");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();

        if(this.__isset.success)
        {
          if (Success != null)
          {
            field.Name = "Success";
            field.Type = TType.Struct;
            field.ID = 0;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await Success.WriteAsync(oprot, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
          }
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as GetTrackAudioResult;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.success == other.__isset.success) && ((!__isset.success) || (System.Object.Equals(Success, other.Success))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.success)
          hashcode = (hashcode * 397) + Success.GetHashCode();
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("GetTrackAudio_result(");
      bool __first = true;
      if (Success != null && __isset.success)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Success: ");
        sb.Append(Success== null ? "<null>" : Success.ToString());
      }
      sb.Append(")");
      return sb.ToString();
    }
  }


  public partial class UploadTrackArgs : TBase
  {
    private TrackAudio _trackAudio;

    public TrackAudio TrackAudio
    {
      get
      {
        return _trackAudio;
      }
      set
      {
        __isset.trackAudio = true;
        this._trackAudio = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool trackAudio;
    }

    public UploadTrackArgs()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.Struct)
              {
                TrackAudio = new TrackAudio();
                await TrackAudio.ReadAsync(iprot, cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("UploadTrack_args");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        if (TrackAudio != null && __isset.trackAudio)
        {
          field.Name = "trackAudio";
          field.Type = TType.Struct;
          field.ID = 1;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await TrackAudio.WriteAsync(oprot, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as UploadTrackArgs;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.trackAudio == other.__isset.trackAudio) && ((!__isset.trackAudio) || (System.Object.Equals(TrackAudio, other.TrackAudio))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.trackAudio)
          hashcode = (hashcode * 397) + TrackAudio.GetHashCode();
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("UploadTrack_args(");
      bool __first = true;
      if (TrackAudio != null && __isset.trackAudio)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("TrackAudio: ");
        sb.Append(TrackAudio== null ? "<null>" : TrackAudio.ToString());
      }
      sb.Append(")");
      return sb.ToString();
    }
  }


  public partial class UploadTrackResult : TBase
  {
    private TrackUploaded _success;

    public TrackUploaded Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool success;
    }

    public UploadTrackResult()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.Struct)
              {
                Success = new TrackUploaded();
                await Success.ReadAsync(iprot, cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("UploadTrack_result");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();

        if(this.__isset.success)
        {
          if (Success != null)
          {
            field.Name = "Success";
            field.Type = TType.Struct;
            field.ID = 0;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await Success.WriteAsync(oprot, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
          }
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as UploadTrackResult;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.success == other.__isset.success) && ((!__isset.success) || (System.Object.Equals(Success, other.Success))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.success)
          hashcode = (hashcode * 397) + Success.GetHashCode();
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("UploadTrack_result(");
      bool __first = true;
      if (Success != null && __isset.success)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Success: ");
        sb.Append(Success== null ? "<null>" : Success.ToString());
      }
      sb.Append(")");
      return sb.ToString();
    }
  }

}
