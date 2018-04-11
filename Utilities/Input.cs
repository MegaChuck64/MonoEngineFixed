using Microsoft.Xna.Framework.Input;
using static Utilities.Structures;

namespace Utilities
{
    public static class Input
    {
        public static MouseState mouse;
        public static MouseState lastMouse;
        public static KeyboardState keyboard;
        public static KeyboardState lastKeyboard;

        public static bool wasPressed(Keys key)
        {
            if (keyboard.IsKeyDown(key) && lastKeyboard.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        public static bool wasPressed(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    if (mouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
                    {
                        return true;
                    }
                    break;
                case MouseButtons.RightButton:
                    if (mouse.RightButton == ButtonState.Pressed && lastMouse.RightButton == ButtonState.Released)
                    {
                        return true;
                    }
                    break;
                case MouseButtons.MiddleButton:
                    if (mouse.MiddleButton == ButtonState.Pressed && lastMouse.MiddleButton == ButtonState.Released)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static bool wasReleased(Keys key)
        {
            if (keyboard.IsKeyUp(key) && lastKeyboard.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public static bool wasReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    if (mouse.LeftButton == ButtonState.Released && lastMouse.LeftButton == ButtonState.Pressed)
                    {
                        return true;
                    }
                    break;
                case MouseButtons.RightButton:
                    if (mouse.RightButton == ButtonState.Released && lastMouse.RightButton == ButtonState.Pressed)
                    {
                        return true;
                    }
                    break;
                case MouseButtons.MiddleButton:
                    if (mouse.MiddleButton == ButtonState.Released && lastMouse.MiddleButton == ButtonState.Pressed)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static int GetAxis(string axis)
        {
            int res = 0;

            switch (axis)
            {
                case "Horizontal":
                    if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
                    {
                        res = -1;
                    }
                    else if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
                    {
                        res = 1;
                    }
                    break;


                case "Vertical":
                    if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
                    {
                        res = -1;
                    }
                    else if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
                    {
                        res = 1;
                    }
                    break;
            }

            return res;
        }

        public static void Begin()
        {
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();
        }

        public static void End()
        {
            lastMouse = mouse;
            lastKeyboard = keyboard;
        }
    }
}
