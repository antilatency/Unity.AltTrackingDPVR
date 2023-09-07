/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace dpn
{
	/// <summary>
	/// Manages an DeePoon head-mounted display (HMD).
	/// </summary>
	public class Buffers
	{
	    private RenderTexture[] _textures = new RenderTexture[_texture_count];
		private IntPtr[] _texture_ptrs = new IntPtr[_texture_count];

		private const int _texture_count = Common.NUM_BUFFER * (int)dpncEYE.NUM;
		private int _current_texture_index = 0;
		private int _next_texture_index = 0;
		
		/// <summary>
		/// Initialization Buffers. Called by DpnManager.
		/// </summary>
		public void Init
			( TEXTURE_DEPTH texture_depth
             , RenderTextureFormat texture_format
			 , int texture_w , int texture_h
			 )
		{
			for (int i = 0; i < _texture_count; i += 2)
			{
				ConfigureEyeTexture( i, dpncEYE.LEFT
				                    , texture_depth
				                    , texture_format
				                    , texture_w , texture_h );
				ConfigureEyeTexture( i, dpncEYE.RIGHT
				                    , texture_depth
				                    , texture_format
				                    , texture_w , texture_h );
			}
		}

		/// <summary>
		/// Updates the internal state of the Buffers. Called by DpnManager.
		/// </summary>
		public bool SwapBuffers()
		{
			bool ret = true;
			for (int i = 0; i < _texture_count; i++)
			{
				if (!_textures[i].IsCreated())
				{
					_textures[i].Create();
					_texture_ptrs[i] = _textures[i].GetNativeTexturePtr();
					ret = false;
				}
			}
			
			_current_texture_index = _next_texture_index;
			_next_texture_index = (_next_texture_index + 2) % _texture_count;

			return ret;
		}

		/// <summary>
		/// Gets the currently active render texture for the given eye.
		/// </summary>
		public RenderTexture GetEyeTexture(dpncEYE eye) { return _textures[_current_texture_index + (int)eye]; }

		/// <summary>
		/// Gets the currently active render texture's native ID for the given eye.
		/// </summary>
		public IntPtr GetEyeTexturePtr(dpncEYE eye) { return _texture_ptrs[_current_texture_index + (int)eye]; }

        // this is used for unity 5.6
        //public IntPtr GetEyeTexturePtr56(dpncEYE eye) { return _texture_ptrs[(_current_texture_index + _texture_count + (int)eye - 2) % _texture_count]; }

        private void ConfigureEyeTexture
			( int buffer_index, dpncEYE eye
			 , TEXTURE_DEPTH texture_depth
			 , RenderTextureFormat texture_format
			 , int texture_w , int texture_h
			 )
		{
			int tex_index = buffer_index + (int)eye;

            _textures[tex_index] = new RenderTexture(texture_w, texture_h, (int)24, texture_format, RenderTextureReadWrite.Linear)
            {
                antiAliasing = Math.Max(QualitySettings.antiAliasing, 1),
            };

            _textures[tex_index].Create();

			_texture_ptrs[tex_index] = _textures[tex_index].GetNativeTexturePtr();
		}

        public void clear ()
        {
            for (int i = 0; i < _textures.Length; i++)
            {
                if (_textures[i].IsCreated())
                {
                    _textures[i].Release();
                    _textures[i] = null;
                    _texture_ptrs[i] = IntPtr.Zero;
                }
            }
        }
	}
}