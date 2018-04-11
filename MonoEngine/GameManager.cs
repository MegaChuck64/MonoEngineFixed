using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace MonoEngine
{
    static public class GameManager
    {

        static List<GameObjectScript> gos = new List<GameObjectScript>();

        


        static void CallScriptMethod(GameObjectScript gos, string methodName)
        {
            MethodInfo methodInfo;
            System.Type type;

            type = gos.GetType();
            methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);


            if (methodInfo != null)
                methodInfo.Invoke(gos, methodInfo.GetParameters());
        }




        static public void Start()
        {
            //gos.Add(new Player());


            foreach (GameObjectScript go in gos)
            {

                go.OnStart();
                CallScriptMethod(go, "Start");
            }
        }



        static public void Update(GameTime gameTime)
        {

            foreach (GameObjectScript go in gos)
            {
                go.OnUpdate(gameTime);

                CallScriptMethod(go, "Update");
            }

        }

        static public void Draw()
        {

            foreach (GameObjectScript go in gos)
            {
                go.OnDraw();
            }
        }
    }
}
