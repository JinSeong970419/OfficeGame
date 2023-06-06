#if HTLIB_CLIENT
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
#endif // HTLIB_CLIENT
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace HT
{
    public class Utils
    {
        private interface HTInvokeElement
        {
            void Invoke();
        }

        private class HTInvokeElement_Param0 : HTInvokeElement
        {
            Action _callback = null;

            public HTInvokeElement_Param0(Action pAction)
            {
                _callback = pAction;
            }

            public void Invoke()
            {
                SafeInvoke(_callback);
            }
        }

        private class HTInvokeElement_Param1<T1> : HTInvokeElement
        {
            Action<T1> _callback = null;
            T1 _param1 = default(T1);

            public HTInvokeElement_Param1(Action<T1> pAction, T1 pParam1)
            {
                _callback = pAction;
                _param1 = pParam1;
            }

            public void Invoke()
            {
                SafeInvoke(_callback, _param1);
            }
        }

        private class HTInvokeElement_Param2<T1, T2> : HTInvokeElement
        {
            Action<T1, T2> _callback = null;
            T1 _param1 = default(T1);
            T2 _param2 = default(T2);

            public HTInvokeElement_Param2(Action<T1, T2> pAction, T1 pParam1, T2 pParam2)
            {
                _callback = pAction;
                _param1 = pParam1;
                _param2 = pParam2;
            }

            public void Invoke()
            {
                SafeInvoke(_callback, _param1, _param2);
            }
        }

        private class HTInvokeElement_Param3<T1, T2, T3> : HTInvokeElement
        {
            Action<T1, T2, T3> _callback = null;
            T1 _param1 = default(T1);
            T2 _param2 = default(T2);
            T3 _param3 = default(T3);

            public HTInvokeElement_Param3(Action<T1, T2, T3> pAction, T1 pParam1, T2 pParam2, T3 pParam3)
            {
                _callback = pAction;
                _param1 = pParam1;
                _param2 = pParam2;
                _param3 = pParam3;
            }

            public void Invoke()
            {
                SafeInvoke(_callback, _param1, _param2, _param3);
            }
        }

        private class HTInvokeElement_Param4<T1, T2, T3, T4> : HTInvokeElement
        {
            Action<T1, T2, T3, T4> _callback = null;
            T1 _param1 = default(T1);
            T2 _param2 = default(T2);
            T3 _param3 = default(T3);
            T4 _param4 = default(T4);

            public HTInvokeElement_Param4(Action<T1, T2, T3, T4> pAction, T1 pParam1, T2 pParam2, T3 pParam3, T4 pParam4)
            {
                _callback = pAction;
                _param1 = pParam1;
                _param2 = pParam2;
                _param3 = pParam3;
                _param4 = pParam4;
            }

            public void Invoke()
            {
                SafeInvoke(_callback, _param1, _param2, _param3, _param4);
            }
        }

        private class HTInvokeElement_Param5<T1, T2, T3, T4, T5> : HTInvokeElement
        {
            Action<T1, T2, T3, T4, T5> _callback = null;
            T1 _param1 = default(T1);
            T2 _param2 = default(T2);
            T3 _param3 = default(T3);
            T4 _param4 = default(T4);
            T5 _param5 = default(T5);

            public HTInvokeElement_Param5(Action<T1, T2, T3, T4, T5> pAction, T1 pParam1, T2 pParam2, T3 pParam3, T4 pParam4, T5 pParam5)
            {
                _callback = pAction;
                _param1 = pParam1;
                _param2 = pParam2;
                _param3 = pParam3;
                _param4 = pParam4;
                _param5 = pParam5;
            }

            public void Invoke()
            {
                SafeInvoke(_callback, _param1, _param2, _param3, _param4, _param5);
            }
        }



        private class HTInvokeElement_Param6<T1, T2, T3, T4, T5, T6> : HTInvokeElement
        {
            Action<T1, T2, T3, T4, T5, T6> _callback = null;
            T1 _param1 = default(T1);
            T2 _param2 = default(T2);
            T3 _param3 = default(T3);
            T4 _param4 = default(T4);
            T5 _param5 = default(T5);
            T6 _param6 = default(T6);

            public HTInvokeElement_Param6(Action<T1, T2, T3, T4, T5, T6> pAction, T1 pParam1, T2 pParam2, T3 pParam3, T4 pParam4, T5 pParam5, T6 pParam6)
            {
                _callback = pAction;
                _param1 = pParam1;
                _param2 = pParam2;
                _param3 = pParam3;
                _param4 = pParam4;
                _param5 = pParam5;
                _param6 = pParam6;
            }

            public void Invoke()
            {
                SafeInvoke(_callback, _param1, _param2, _param3, _param4, _param5, _param6);
            }
        }



        //---------------------------------------
        static private List<HTInvokeElement> _invokeElements = new List<HTInvokeElement>();
        static private System.Object _invokeLock = new System.Object();
        static public void InvokeAll()
        {
            lock (_invokeLock)
            {
                for (int nInd = 0; nInd < _invokeElements.Count; ++nInd)
                    _invokeElements[nInd].Invoke();

                _invokeElements.Clear();
            }
        }

        /////////////////////////////////////////
        //---------------------------------------
        /// <summary>
        /// CallbackÀ» ¾ÈÀüÇÏ°Ô InvokeÇÕ´Ï´Ù.
        /// Invoke È£Ãâ À§Ä¡°¡ Main Thread°¡ ¾Æ´Ò °æ¿ì Áö¿¬È£ÃâµË´Ï´Ù.
        /// </summary>
        static public bool SafeInvoke(Action pCallback)
        {
            if (pCallback != null)
            {
#if HTLIB_CLIENT
				if (HTFramework.Instanced != null && HTFramework.Instance.IsMainThead() == false)
				{
					lock (_invokeLock)
						_invokeElements.Add(new HTInvokeElement_Param0(pCallback));
				}
				else
#endif // HTLIB_CLIENT
                pCallback();

                return true;
            }

            return false;
        }

        /// <summary>
        /// CallbackÀ» ¾ÈÀüÇÏ°Ô InvokeÇÕ´Ï´Ù.
        /// Invoke È£Ãâ À§Ä¡°¡ Main Thread°¡ ¾Æ´Ò °æ¿ì Áö¿¬È£ÃâµË´Ï´Ù.
        /// </summary>
        static public bool SafeInvoke<T>(Action<T> pCallback, T tParam)
        {
            if (pCallback != null)
            {
#if HTLIB_CLIENT
				if (HTFramework.Instanced != null && HTFramework.Instance.IsMainThead() == false)
				{
					lock (_invokeLock)
						_invokeElements.Add(new HTInvokeElement_Param1<T>(pCallback, tParam));
				}
				else
#endif // HTLIB_CLIENT
                pCallback(tParam);

                return true;
            }

            return false;
        }

        /// <summary>
        /// CallbackÀ» ¾ÈÀüÇÏ°Ô InvokeÇÕ´Ï´Ù.
        /// Invoke È£Ãâ À§Ä¡°¡ Main Thread°¡ ¾Æ´Ò °æ¿ì Áö¿¬È£ÃâµË´Ï´Ù.
        /// </summary>
        static public bool SafeInvoke<T1, T2>(Action<T1, T2> pCallback, T1 tParam1, T2 tParam2)
        {
            if (pCallback != null)
            {
#if HTLIB_CLIENT
				if (HTFramework.Instanced != null && HTFramework.Instance.IsMainThead() == false)
				{
					lock (_invokeLock)
						_invokeElements.Add(new HTInvokeElement_Param2<T1, T2>(pCallback, tParam1, tParam2));
				}
				else
#endif // HTLIB_CLIENT
                pCallback(tParam1, tParam2);

                return true;
            }

            return false;
        }

        /// <summary>
        /// CallbackÀ» ¾ÈÀüÇÏ°Ô InvokeÇÕ´Ï´Ù.
        /// Invoke È£Ãâ À§Ä¡°¡ Main Thread°¡ ¾Æ´Ò °æ¿ì Áö¿¬È£ÃâµË´Ï´Ù.
        /// </summary>
        static public bool SafeInvoke<T1, T2, T3>(Action<T1, T2, T3> pCallback, T1 tParam1, T2 tParam2, T3 tParam3)
        {
            if (pCallback != null)
            {
#if HTLIB_CLIENT
				if (HTFramework.Instanced != null && HTFramework.Instance.IsMainThead() == false)
				{
					lock (_invokeLock)
						_invokeElements.Add(new HTInvokeElement_Param3<T1, T2, T3>(pCallback, tParam1, tParam2, tParam3));
				}
				else
#endif // HTLIB_CLIENT
                pCallback(tParam1, tParam2, tParam3);

                return true;
            }

            return false;
        }

        /// <summary>
        /// CallbackÀ» ¾ÈÀüÇÏ°Ô InvokeÇÕ´Ï´Ù.
        /// Invoke È£Ãâ À§Ä¡°¡ Main Thread°¡ ¾Æ´Ò °æ¿ì Áö¿¬È£ÃâµË´Ï´Ù.
        /// </summary>
        static public bool SafeInvoke<T1, T2, T3, T4>(Action<T1, T2, T3, T4> pCallback, T1 tParam1, T2 tParam2, T3 tParam3, T4 tParam4)
        {
            if (pCallback != null)
            {
#if HTLIB_CLIENT
				if (HTFramework.Instanced != null && HTFramework.Instance.IsMainThead() == false)
				{
					lock (_invokeLock)
						_invokeElements.Add(new HTInvokeElement_Param4<T1, T2, T3, T4>(pCallback, tParam1, tParam2, tParam3, tParam4));
				}
				else
#endif // HTLIB_CLIENT
                pCallback(tParam1, tParam2, tParam3, tParam4);

                return true;
            }

            return false;
        }

        /// <summary>
        /// CallbackÀ» ¾ÈÀüÇÏ°Ô InvokeÇÕ´Ï´Ù.
        /// Invoke È£Ãâ À§Ä¡°¡ Main Thread°¡ ¾Æ´Ò °æ¿ì Áö¿¬È£ÃâµË´Ï´Ù.
        /// </summary>
        static public bool SafeInvoke<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> pCallback, T1 tParam1, T2 tParam2, T3 tParam3, T4 tParam4, T5 tParam5)
        {
            if (pCallback != null)
            {
#if HTLIB_CLIENT
				if (HTFramework.Instanced != null && HTFramework.Instance.IsMainThead() == false)
				{
					lock (_invokeLock)
						_invokeElements.Add(new HTInvokeElement_Param5<T1, T2, T3, T4, T5>(pCallback, tParam1, tParam2, tParam3, tParam4, tParam5));
				}
				else
#endif // HTLIB_CLIENT
                pCallback(tParam1, tParam2, tParam3, tParam4, tParam5);

                return true;
            }

            return false;
        }

        /// <summary>
        /// CallbackÀ» ¾ÈÀüÇÏ°Ô InvokeÇÕ´Ï´Ù.
        /// Invoke È£Ãâ À§Ä¡°¡ Main Thread°¡ ¾Æ´Ò °æ¿ì Áö¿¬È£ÃâµË´Ï´Ù.
        /// </summary>
        static public bool SafeInvoke<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> pCallback, T1 tParam1, T2 tParam2, T3 tParam3, T4 tParam4, T5 tParam5, T6 tParam6)
        {
            if (pCallback != null)
            {
#if HTLIB_CLIENT
				if (HTFramework.Instanced != null && HTFramework.Instance.IsMainThead() == false)
				{
					lock (_invokeLock)
						_invokeElements.Add(new HTInvokeElement_Param6<T1, T2, T3, T4, T5, T6>(pCallback, tParam1, tParam2, tParam3, tParam4, tParam5, tParam6));
				}
				else
#endif // HTLIB_CLIENT
                pCallback(tParam1, tParam2, tParam3, tParam4, tParam5, tParam6);

                return true;
            }

            return false;
        }



        //---------------------------------------
#if HTLIB_CLIENT
		/// <summary>
		/// CallbackÀ» Æ¯Á¤ ½Ã°£ ÈÄ¿¡ InvokeÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeInvokeDelay(float fTime, Action pCallback)
		{
			if (pCallback != null)
				StopWatch.CreateNewStopWatch(fTime, false, () => { SafeInvoke(pCallback); });
		}

		/// <summary>
		/// CallbackÀ» Æ¯Á¤ ½Ã°£ ÈÄ¿¡ InvokeÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeInvokeDelay<T>(float fTime, Action<T> pCallback, T tParam)
		{
			if (pCallback != null)
				StopWatch.CreateNewStopWatch(fTime, false, () => { SafeInvoke(pCallback, tParam); });
		}

		/// <summary>
		/// CallbackÀ» Æ¯Á¤ ½Ã°£ ÈÄ¿¡ InvokeÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeInvokeDelay<T1, T2>(float fTime, Action<T1, T2> pCallback, T1 tParam1, T2 tParam2)
		{
			if (pCallback != null)
				StopWatch.CreateNewStopWatch(fTime, false, () => { SafeInvoke(pCallback, tParam1, tParam2); });
		}

		/// <summary>
		/// CallbackÀ» Æ¯Á¤ ½Ã°£ ÈÄ¿¡ InvokeÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeInvokeDelay<T1, T2, T3>(float fTime, Action<T1, T2, T3> pCallback, T1 tParam1, T2 tParam2, T3 tParam3)
		{
			if (pCallback != null)
				StopWatch.CreateNewStopWatch(fTime, false, () => { SafeInvoke(pCallback, tParam1, tParam2, tParam3); });
		}

		//---------------------------------------
		/// <summary>
		/// CallbackÀ» Æ¯Á¤ ½Ã°£ ÈÄ¿¡ InvokeÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeInvokeDelayRealtime(float fTime, Action pCallback)
		{
			if (pCallback != null)
				StopWatch.CreateNewStopWatch(fTime, true, () => { SafeInvoke(pCallback); });
		}

		/// <summary>
		/// CallbackÀ» Æ¯Á¤ ½Ã°£ ÈÄ¿¡ InvokeÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeInvokeDelayRealtime<T>(float fTime, Action<T> pCallback, T tParam)
		{
			if (pCallback != null)
				StopWatch.CreateNewStopWatch(fTime, true, () => { SafeInvoke(pCallback, tParam); });
		}

		/// <summary>
		/// CallbackÀ» Æ¯Á¤ ½Ã°£ ÈÄ¿¡ InvokeÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeInvokeDelayRealtime<T1, T2>(float fTime, Action<T1, T2> pCallback, T1 tParam1, T2 tParam2)
		{
			if (pCallback != null)
				StopWatch.CreateNewStopWatch(fTime, true, () => { SafeInvoke(pCallback, tParam1, tParam2); });
		}

		/// <summary>
		/// CallbackÀ» Æ¯Á¤ ½Ã°£ ÈÄ¿¡ InvokeÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeInvokeDelayRealtime<T1, T2, T3>(float fTime, Action<T1, T2, T3> pCallback, T1 tParam1, T2 tParam2, T3 tParam3)
		{
			if (pCallback != null)
				StopWatch.CreateNewStopWatch(fTime, true, () => { SafeInvoke(pCallback, tParam1, tParam2, tParam3); });
		}

#endif // HTLIB_CLIENT

        //---------------------------------------
        /// <summary>
        /// CallBack¿¡ AddFunc¸¦ µî·Ï ÇÑ µÚ AddFunc¸¦ È£ÃâÇÕ´Ï´Ù.
        /// CallBack¿¡ µî·Ï°ú È£ÃâÀÌ µ¿½Ã¿¡ µÇ¾ßÇÏ´Â Initialize °úÁ¤¿¡ ÀûÀýÇÕ´Ï´Ù.
        /// </summary>
        /// <param name="pCallback">AddFunc°¡ Ãß°¡ µÉ ´ë»ó Callback</param>
        /// <param name="pAddFunc">Ãß°¡ µÈ µÚ È£Ãâ µÉ AddFunc</param>
        static public void SafeInvokeAndAdd(ref Action pCallback, Action pAddFunc)
        {
            if (pAddFunc == null)
                return;

            pCallback += pAddFunc;
            pAddFunc();
        }

        /// <summary>
        /// CallBack¿¡ AddFunc¸¦ µî·Ï ÇÑ µÚ AddFunc¸¦ È£ÃâÇÕ´Ï´Ù.
        /// CallBack¿¡ µî·Ï°ú È£ÃâÀÌ µ¿½Ã¿¡ µÇ¾ßÇÏ´Â Initialize °úÁ¤¿¡ ÀûÀýÇÕ´Ï´Ù.
        /// </summary>
        /// <param name="pCallback">AddFunc°¡ Ãß°¡ µÉ ´ë»ó Callback</param>
        /// <param name="pAddFunc">Ãß°¡ µÈ µÚ È£Ãâ µÉ AddFunc</param>
        static public void SafeInvokeAndAdd<T>(ref Action<T> pCallback, Action<T> pAddFunc, T tParam)
        {
            if (pAddFunc == null)
                return;

            pCallback += pAddFunc;
            pAddFunc(tParam);
        }

        /// <summary>
        /// CallBack¿¡ AddFunc¸¦ µî·Ï ÇÑ µÚ AddFunc¸¦ È£ÃâÇÕ´Ï´Ù.
        /// CallBack¿¡ µî·Ï°ú È£ÃâÀÌ µ¿½Ã¿¡ µÇ¾ßÇÏ´Â Initialize °úÁ¤¿¡ ÀûÀýÇÕ´Ï´Ù.
        /// </summary>
        /// <param name="pCallback">AddFunc°¡ Ãß°¡ µÉ ´ë»ó Callback</param>
        /// <param name="pAddFunc">Ãß°¡ µÈ µÚ È£Ãâ µÉ AddFunc</param>
        static public void SafeInvokeAndAdd<T1, T2>(ref Action<T1, T2> pCallback, Action<T1, T2> pAddFunc, T1 tParam1, T2 tParam2)
        {
            if (pAddFunc == null)
                return;

            pCallback += pAddFunc;
            pAddFunc(tParam1, tParam2);
        }

        /// <summary>
        /// CallBack¿¡ AddFunc¸¦ µî·Ï ÇÑ µÚ AddFunc¸¦ È£ÃâÇÕ´Ï´Ù.
        /// CallBack¿¡ µî·Ï°ú È£ÃâÀÌ µ¿½Ã¿¡ µÇ¾ßÇÏ´Â Initialize °úÁ¤¿¡ ÀûÀýÇÕ´Ï´Ù.
        /// </summary>
        /// <param name="pCallback">AddFunc°¡ Ãß°¡ µÉ ´ë»ó Callback</param>
        /// <param name="pAddFunc">Ãß°¡ µÈ µÚ È£Ãâ µÉ AddFunc</param>
        static public void SafeInvokeAndAdd<T1, T2, T3>(ref Action<T1, T2, T3> pCallback, Action<T1, T2, T3> pAddFunc, T1 tParam1, T2 tParam2, T3 tParam3)
        {
            if (pAddFunc == null)
                return;

            pCallback += pAddFunc;
            pAddFunc(tParam1, tParam2, tParam3);
        }


        /////////////////////////////////////////
        //---------------------------------------
#if HTLIB_CLIENT
		/// <summary>
		/// GameObjectÀÇ Active State¸¦ ¾ÈÀüÇÏ°Ô º¯°æÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeActive(GameObject pObject, bool bActive)
		{
			if (pObject != null && pObject.activeSelf != bActive)
				pObject.SetActive(bActive);
		}

		/// <summary>
		/// GameObject ¹è¿­ÀÇ Active State¸¦ ¾ÈÀüÇÏ°Ô º¯°æÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeActive(GameObject[] vObject, bool bActive)
		{
			for (int nInd = 0; nInd < vObject.Length; ++nInd)
				SafeActive(vObject[nInd], bActive);
		}

		/// <summary>
		/// GameObject ¹è¿­ÀÇ Active State¸¦ ¾ÈÀüÇÏ°Ô º¯°æÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeActive(List<GameObject> vObject, bool bActive)
		{
			for (int nInd = 0; nInd < vObject.Count; ++nInd)
				SafeActive(vObject[nInd], bActive);
		}

		/// <summary>
		/// MonoBehaviour°¡ µî·ÏµÇ¾îÀÖ´Â GameObjectÀÇ Active State¸¦ ¾ÈÀüÇÏ°Ô º¯°æÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeActive(Component pObject, bool bActive)
		{
			if (pObject != null && pObject.gameObject.activeSelf != bActive)
				pObject.gameObject.SetActive(bActive);
		}

		/// <summary>
		/// MonoBehaviour°¡ µî·ÏµÇ¾îÀÖ´Â GameObject ¹è¿­ÀÇ Active State¸¦ ¾ÈÀüÇÏ°Ô º¯°æÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeActive(Component[] vObject, bool bActive)
		{
            if (vObject != null)
            {
                for (int nInd = 0; nInd < vObject.Length; ++nInd)
                    SafeActive(vObject[nInd], bActive);
            }
		}

		/// <summary>
		/// MonoBehaviour°¡ µî·ÏµÇ¾îÀÖ´Â GameObject ¹è¿­ÀÇ Active State¸¦ ¾ÈÀüÇÏ°Ô º¯°æÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeActive<T>(List<T> vObject, bool bActive) where T : Component
        {
            if (vObject != null)
            {
                for (int nInd = 0; nInd < vObject.Count; ++nInd)
                    SafeActive(vObject[nInd], bActive);
            }
		}

		//---------------------------------------
		/// <summary>
		/// MonoBehaviourÀÇ Enable State¸¦ ¾ÈÀüÇÏ°Ô º¯°æÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeEnable(Behaviour pObject, bool bActive)
		{
			if (pObject != null && pObject.enabled != bActive)
				pObject.enabled = bActive;
		}

		/// <summary>
		/// MonoBehaviourÀÇ Enable State¸¦ ¾ÈÀüÇÏ°Ô º¯°æÇÕ´Ï´Ù.
		/// </summary>
		static public void SafeEnable(Behaviour[] vObject, bool bActive)
		{
			for (int nInd = 0; nInd < vObject.Length; ++nInd)
				SafeEnable(vObject[nInd], bActive);
		}

		//---------------------------------------
		/// <summary>
		/// GameObject¸¦ ¾ÈÀüÇÏ°Ô ÆÄ±«ÇÕ´Ï´Ù.
		/// Object Pool¿¡ ±Í¼Ó µÈ GameObject´Â Object Pool·Î È¯¿øµË´Ï´Ù.
		/// </summary>
		static public bool SafeDestroy(GameObject pObject)
		{
			if (pObject == null)
				return false;

			if (ObjectPool.ReturnToPool(pObject))
				return true;

			GameObject.Destroy(pObject);
			return true;
		}

		/// <summary>
		/// GameObject¸¦ ¾ÈÀüÇÏ°Ô ÆÄ±«ÇÕ´Ï´Ù.
		/// Object Pool¿¡ ±Í¼Ó µÈ GameObject´Â Object Pool·Î È¯¿øµË´Ï´Ù.
		/// </summary>
		static public bool SafeDestroy(Component pObject)
		{
			if (pObject == null)
				return false;

			return SafeDestroy(pObject.gameObject);
		}

		/// <summary>
		/// GameObject¸¦ Æ¯Á¤ ½Ã°£ Áö¿¬ ÈÄ¿¡ ¾ÈÀüÇÏ°Ô ÆÄ±«ÇÕ´Ï´Ù.
		/// Object Pool¿¡ ±Í¼Ó µÈ GameObject´Â Object Pool·Î È¯¿øµË´Ï´Ù.
		/// </summary>
		/// <param name="fDelay">ÆÄ±« Áö¿¬ ½Ã°£</param>
		static public bool SafeDestroy(GameObject pObject, float fDelay)
		{
			return SafeDestroy(pObject, fDelay, false);
		}

		/// <summary>
		/// GameObject¸¦ Æ¯Á¤ ½Ã°£ Áö¿¬ ÈÄ¿¡ ¾ÈÀüÇÏ°Ô ÆÄ±«ÇÕ´Ï´Ù.
		/// Object Pool¿¡ ±Í¼Ó µÈ GameObject´Â Object Pool·Î È¯¿øµË´Ï´Ù.
		/// </summary>
		/// <param name="fDelay">ÆÄ±« Áö¿¬ ½Ã°£</param>
		static public bool SafeDestroy(Component pObject, float fDelay)
		{
			if (pObject == null)
				return false;

			return SafeDestroy(pObject.gameObject, fDelay, false);
		}

		/// <summary>
		/// GameObject¸¦ Æ¯Á¤ ½Ã°£ Áö¿¬ ÈÄ¿¡ ¾ÈÀüÇÏ°Ô ÆÄ±«ÇÕ´Ï´Ù.
		/// Object Pool¿¡ ±Í¼Ó µÈ GameObject´Â Object Pool·Î È¯¿øµË´Ï´Ù.
		/// </summary>
		/// <param name="fDelay">ÆÄ±« Áö¿¬ ½Ã°£</param>
		/// <param name="bRealTime">Áö¿¬ ½Ã°£¿¡ Time Scale Àû¿ë ¿©ºÎÀÔ´Ï´Ù</param>
		static public bool SafeDestroy(GameObject pObject, float fDelay, bool bRealTime)
		{
			if (pObject == null)
				return false;

			if (ObjectPool.IsPoolObject(pObject))
			{
				StopWatch.CreateNewStopWatch(fDelay, bRealTime, () => { ObjectPool.ReturnToPool(pObject); });
				return true;
			}

			GameObject.Destroy(pObject, fDelay);
			return true;
		}

		/// <summary>
		/// GameObject¸¦ Æ¯Á¤ ½Ã°£ Áö¿¬ ÈÄ¿¡ ¾ÈÀüÇÏ°Ô ÆÄ±«ÇÕ´Ï´Ù.
		/// Object Pool¿¡ ±Í¼Ó µÈ GameObject´Â Object Pool·Î È¯¿øµË´Ï´Ù.
		/// </summary>
		/// <param name="fDelay">ÆÄ±« Áö¿¬ ½Ã°£</param>
		/// <param name="bRealTime">Áö¿¬ ½Ã°£¿¡ Time Scale Àû¿ë ¿©ºÎÀÔ´Ï´Ù</param>
		static public bool SafeDestroy(Component pObject, float fDelay, bool bRealTime)
		{
			if (pObject == null)
				return false;

			return SafeDestroy(pObject.gameObject, fDelay, bRealTime);
		}


		/////////////////////////////////////////
		//---------------------------------------
		/// <summary>
		/// CoroutineÀ» ¾ÈÀüÇÏ°Ô Á¤Áö½ÃÅµ´Ï´Ù.
		/// </summary>
		/// <param name="pParent">CoroutineÀÌ ±Í¼Ó µÈ MonoBehaviour</param>
		/// <param name="pCorutine">´ë»ó Coroutine</param>
		static public void SafeStopCorutine(MonoBehaviour pParent, ref Coroutine pCorutine)
		{
			do
			{
				if (pParent == null || pCorutine == null)
					break;

				pParent.StopCoroutine(pCorutine);
			}
			while (false);

			pCorutine = null;
		}


		/////////////////////////////////////////
		//---------------------------------------
		/// <summary>
		/// Resource¿¡¼­ Load µÈ GameObject¸¦ ObjectPoolÀ» »ç¿ëÇØ º¹Á¦ »ý¼ºÇÕ´Ï´Ù.
		/// </summary>
		static public GameObject InstantiateFromPool(string szResourceAddress, Transform pInheritTransform = null, bool bInheritPos = false, bool bInheritRot = false)
		{
			if (HTFramework.GameClosed)
				return null;

			return ObjectPool.InstantiateObject(szResourceAddress, pInheritTransform, bInheritPos, bInheritRot);
		}

		/// <summary>
		/// ObjectPoolÀ» »ç¿ëÇØ º¹Á¦ »ý¼ºÇÕ´Ï´Ù.
		/// </summary>
		static public GameObject InstantiateFromPool(GameObject pOriginal, Transform pInheritTransform = null, bool bInheritPos = false, bool bInheritRot = false)
		{
			if (HTFramework.GameClosed)
				return null;

			if (pOriginal == null)
				return null;

			return ObjectPool.InstantiateObject(pOriginal, pInheritTransform, bInheritPos, bInheritRot);
		}

		/// <summary>
		/// ObjectPoolÀ» »ç¿ëÇØ º¹Á¦ »ý¼ºÇÕ´Ï´Ù.
		/// </summary>
		static public T InstantiateFromPool<T>(T pOriginal, Transform pInheritTransform = null, bool bInheritPos = false, bool bInheritRot = false) where T : MonoBehaviour
		{
			if (HTFramework.GameClosed)
				return null;

			if (pOriginal == null)
				return null;

			GameObject pObj = ObjectPool.InstantiateObject(pOriginal.gameObject, pInheritTransform, bInheritPos, bInheritRot);

            if (pObj == null)
            {
                return null;
                //HTDebug.PrintLog(eMessageType.Error, "TESTTESTTEST");
            }

			return pObj.GetComponent<T>();
		}

		/// <summary>
		/// ObjectPoolÀ» »ç¿ëÇØ º¹Á¦ »ý¼ºÇÕ´Ï´Ù.
		/// </summary>
		static public ParticleSystem InstantiateFromPool(ParticleSystem pOriginal, Transform pInheritTransform = null, bool bInheritPos = false, bool bInheritRot = false)
		{
			if (HTFramework.GameClosed)
				return null;

			if (pOriginal == null)
				return null;

			GameObject pObj = ObjectPool.InstantiateObject(pOriginal.gameObject, pInheritTransform, bInheritPos, bInheritRot);
			if (pObj == null)
				return null;

			return pObj.GetComponent<ParticleSystem>();
		}

		//---------------------------------------
		/// <summary>
		/// Resource¿¡¼­ Load µÈ GameObject¸¦ º¹Á¦ »ý¼ºÇÕ´Ï´Ù.
		/// </summary>
		static public GameObject InstantiateForNetwork(string szResourceAddress, bool instantiateOnSlave=false)
		{
			if (HTFramework.GameClosed)
				return null;

			szResourceAddress = szResourceAddress.Replace("\\", "/").Replace("//", "/");

			GameObject g = null;
			if (RuleManager.Instance!=null
				&& RuleManager.Instance.CurrentGameRule!=null
				&& RuleManager.Instance.CurrentGameRule.IsNetworkPlay)
			{
				if (Photon.Pun.PhotonNetwork.IsMasterClient || instantiateOnSlave)
				{
					GameObject go = Photon.Pun.PhotonNetwork.Instantiate(szResourceAddress, Vector3.zero, Quaternion.identity);

                    if (go == null)
                    {
						HTDebug.PrintLog(eMessageType.Error, "Failed to create network gameobject=" + szResourceAddress);
						return null;
					}

					NavMeshAgent a = go.GetComponent<NavMeshAgent>();
					if (a != null) a.enabled = true;

					return go;
                }
                else
                {
					HTDebug.PrintLog(eMessageType.Warning, "Only master instantiates objects on network play mode. gameobject=" + szResourceAddress);
					return null;
                }
			}
			else
			{
				
				GameObject go = Instantiate(ResourceUtils.Load<GameObject>(szResourceAddress));

				if (go == null)
				{
					HTDebug.PrintLog(eMessageType.Error, "Failed to create gameobject=" + szResourceAddress);
					return null;
				}

				return go;

			}
		}




		static public GameObject Instantiate(string szResourceAddress, bool addNetworkSync = true)
		{
			if (HTFramework.GameClosed)
				return null;

			GameObject go = Instantiate(ResourceUtils.Load<GameObject>(szResourceAddress), addNetworkSync);

			if (go == null)
			{
				HTDebug.PrintLog(eMessageType.Error, "Failed to create gameobject=" + szResourceAddress);
				return null;
			}

			return go;

		}



		/// <summary>
		/// GameObject¸¦ º¹Á¦ »ý¼ºÇÕ´Ï´Ù.
		/// </summary>
		static public GameObject Instantiate(GameObject pOriginal, bool addNetworkSync=true, bool? isMine=null)
		{
			if (HTFramework.GameClosed)
				return null;

			if (pOriginal == null)
				return null;

			GameObject g = GameObject.Instantiate(pOriginal);

            //if (isObjectType == false)
            //{
			NavMeshAgent a = g.GetComponent<NavMeshAgent>();
			if (a != null) a.enabled = true;
			//}

			if (addNetworkSync)
			{
				PhotonObjectManager.Instance.AddGameObject(g, pOriginal.name, true, isMine);
			}

			return g;
        }

		/// <summary>
		/// GameObject¸¦ º¹Á¦ »ý¼ºÇÕ´Ï´Ù.
		/// </summary>
		static public T Instantiate<T>(T pOriginal) where T : Component
		{
			if (HTFramework.GameClosed)
				return null;

			if (pOriginal == null)
				return null;

			GameObject pInst = Instantiate(pOriginal.gameObject);
			return pInst.GetComponent<T>();
		}

		/// <summary>
		/// GameObject¸¦ º¹Á¦ »ý¼ºÇÕ´Ï´Ù.
		/// </summary>
		static public ParticleSystem Instantiate(ParticleSystem pOriginal)
		{
			if (HTFramework.GameClosed)
				return null;

			if (pOriginal == null)
				return null;

			GameObject pInst = Instantiate(pOriginal.gameObject);
			return pInst.GetComponent<ParticleSystem>();
		}

		/// <summary>
		/// ºñ¾îÀÖ´Â GameObject¸¦ »ý¼ºÇÕ´Ï´Ù.
		/// </summary>
		static public GameObject Instantiate()
		{
			if (HTFramework.GameClosed)
				return null;

			return new GameObject();
		}

		public static string ArrayToString<T>(T[] array, string separator)
		{
			string str = "";
			foreach (T s in array)
			{
				str += (s==null ? "null" : s.ToString()) + separator;
			}

			if (str.Length > 0)
			{
				str = str.Substring(0, str.Length - separator.Length);
			}

			return str;
		}



		/////////////////////////////////////////
		//---------------------------------------
		/// <summary>
		/// ÇØ´ç GameObjectÀÇ TransformÀ» ÂüÁ¶ ´ë»ó Transform°ú ¸ÂÃä´Ï´Ù.
		/// </summary>
		/// <param name="pTransform">ÂüÁ¶ ´ë»ó Transform</param>
		/// <param name="bInheritPos">´ë»ó TransformÀÇ À§Ä¡¸¦ ÂüÁ¶ÇÕ´Ï´Ù</param>
		/// <param name="bInheritRot">´ë»ó TransformÀÇ È¸ÀüÀ» ÂüÁ¶ÇÕ´Ï´Ù</param>
		/// <param name="bInheritScale">´ë»ó TransformÀÇ Å©±â¸¦ ÂüÁ¶ÇÕ´Ï´Ù</param>
		/// <param name="bInheritParent">´ë»ó TransformÀÇ Hierarchy ÇÏÀ§¿¡ À§Ä¡½ÃÅµ´Ï´Ù</param>
		static public void InheritTransform(GameObject pObject, Transform pTransform, bool bInheritPos = false, bool bInheritRot = false, bool bInheritScale = false, bool bInheritParent = false)
		{
			if (pObject == null || pTransform == null)
				return;

			if (bInheritPos)
				pObject.transform.position = pTransform.position;

			if (bInheritRot)
				pObject.transform.rotation = pTransform.rotation;

			if (bInheritParent)
				pObject.transform.SetParent(pTransform);

			if (bInheritScale)
				pObject.transform.localScale = pTransform.localScale;
		}

		// ¹®ÀÚ¿­³» Æ¯¼ö¹®ÀÚ ÀÖ´ÂÁö Ã¼Å©
		// ±¹³» ¼­ºñ½º½Ã ÇÑ±ÛÀº Åë°ú½ÃÅ°Áö¸¸ ÇØ¿Ü ¼­ºñ½º½Ã ÇÑ±Ûµµ Ã³¸®ÇØ¾ßÇÔ
		static public bool CheckSpecialChar(string szCheckStr)
		{
#if SINDORIM_CLIENT && !SCENARIO_EDITOR
            // ´Ð³×ÀÓ Ã¼Å©½Ã ¼­¹ö ·ÎÁ÷¿¡ µû¶ó ÀÏ°üµÇ°Ô ÆÇ´ÜµÇ¾î¾ß ÇÏ°í,
            // ÇöÀç ÀÌ ÇÔ¼ö°¡ megaport µî Æ¯¼ö¹®ÀÚ ¾Æ´Ñ °æ¿ì¿¡µµ true ¸®ÅÏÇÏ¿©
            // ÁÖ¼®Ã³¸®ÇÔ.
            /*
			if (string.IsNullOrEmpty(szCheckStr))
				return true;

			for (int nInd = 0; nInd < szCheckStr.Length; ++nInd)
				if (!System.Text.RegularExpressions.Regex.IsMatch(szCheckStr, @"^[°¡-ÆRa-zA-Z0-9]*$"))
					return true;

			Dictionary<int, BannedWordsTableData> vBannedWords = GameTable_BannedWords.Instance.FindBannedWordsData();
			foreach (BannedWordsTableData pData in vBannedWords.Values)
			{
				for (int nInd = 0; nInd < pData.tid.Length; ++nInd)
				{
					if (string.IsNullOrEmpty(pData.tid[nInd]))
						continue;

					if (szCheckStr.Contains(pData.tid[nInd]))
						return true;
				}
			}

			return false;
            */
            return false;

#else //  SINDORIM_CLIENT && !SCENARIO_EDITOR
			return false;

#endif // SINDORIM_CLIENT && !SCENARIO_EDITOR
        }

        static public void MissionLocationUIOpen(eMissionLocationType.e eType, Action action = null)
		{
			switch (eType)
			{
				case eMissionLocationType.e.EMPTY:
					break;

				case eMissionLocationType.e.ADVENTURE:
					{
						UIPopup_StageSelect pPopup = HTUIManager.OpenPopup("UIPopup_StageSelect") as UIPopup_StageSelect;
						pPopup.openAction(action);
					}
					break;

				case eMissionLocationType.e.CHALLENGE:
					{
						HTUIManager.CloseAllPopup();
						UIPopup_Challenge pPopup = HTUIManager.OpenPopup("UIPopup_Operation") as UIPopup_Challenge;
					}
					break;

				case eMissionLocationType.e.CHARACTER:
					{
						UIPopup_Character pPopup = HTUIManager.OpenPopup("UIPopup_Character") as UIPopup_Character;
					}
					break;

				case eMissionLocationType.e.GACHA:
					{
						UIPopup_Store_Actor pPopup = HTUIManager.OpenPopup("UIPopup_Store_Actor") as UIPopup_Store_Actor;
						pPopup.OnOpen(eStoreCategory.e.Summons, eStoreMenu.e.Summons_Premium);
					}
					break;

				case eMissionLocationType.e.EQUIPMENTS:
					{
						UIPopup_Inventory pPopup = HTUIManager.OpenPopup("UIPopup_Inventory") as UIPopup_Inventory;
					}
					break;

				case eMissionLocationType.e.ARENA:
                    {
						HTUIManager.CloseAllPopup();
						// ¾Æ·¹³ª¸¸ ±¸Á¶°¡ ´Þ¶ó¼­ CallRegister »ç¿ë
						UIPopupCallRegister callRegister = new UIPopupCallRegister();
						callRegister.SetAddressToOpenPopup("UIPopup_Operation");
                    }
					break;
			}
		}

		static public void StoreOpen_Direct(eStoreCategory.e eCategoryType, eStoreMenu.e eMenu)
		{
			UIPopup_Store pStore = null;
			if (HTUIManager.OpenedPopupList != null && HTUIManager.OpenedPopupList.Count > 0)
			{
				for (int nInd = 0; nInd < HTUIManager.OpenedPopupList.Count; ++nInd)
				{
					if (HTUIManager.OpenedPopupList[nInd].GetType() == typeof(UIPopup_Store))
					{
						pStore = HTUIManager.OpenedPopupList[nInd] as UIPopup_Store;

						break;
					}
				}
			}

			if (pStore != null)
				pStore.RefreshByTab(eCategoryType, eMenu);
			else
			{
				UIPopup_Store pPopup = null;
				if (eCategoryType == eStoreCategory.e.Summons)
					pPopup = HTUIManager.OpenPopup("UIPopup_Store_Actor") as UIPopup_Store_Actor;
				else
					pPopup = HTUIManager.OpenPopup("UIPopup_Store") as UIPopup_Store;

				if (pPopup != null)
					pPopup.OnOpen(eCategoryType, eMenu);

			}
		}

		static public void StoreOpen(eStoreCategory.e eCategoryType, eStoreMenu.e eMenu, string szDesc)
		{
			UIPopup_Notice pPopup = HTUIManager.OpenPopup("UIPopup_Notice") as UIPopup_Notice;
			pPopup.Set(szDesc, "Store_Move", "Cmn_No_Btn", () =>
			{
				StoreOpen_Direct(eCategoryType, eMenu);
			});
		}

		static public void StoreOpen_PopupStore(StoreTableData_Product pProduct)
		{
			UIPopup_PopupStore pPopup = HTUIManager.OpenPopup("UIPopup_PopupStore") as UIPopup_PopupStore;
			pPopup.OnOpen(pProduct);
		}

		static public void GameInit()
		{
			UserDataManager.Instance.ResetMyUserData();
			LoadGameTitle();
		}

		static public void LoadGameTitle()
		{
			GameManager.Instance.ChangeSceneToTitle();
		}


        static public string GetRankText(int rank)
        {
            if (rank <= 0)
                return HTLocaleTable.GetLocalstring("PVP_Text_No_Ranking");

            return string.Format(HTLocaleTable.GetLocalstring("GameContents_Challenge_Text_Current_Rank"), rank);
        }

        static public string GetRankTextOnlyNumber(int rank)
        {
            if (rank <= 0)
                return HTLocaleTable.GetLocalstring("PVP_Text_No_Ranking");
            else
                return rank.ToString();
        }


        /////////////////////////////////////////
        //---------------------------------------
#endif // HTLIB_CLIENT
    }
}