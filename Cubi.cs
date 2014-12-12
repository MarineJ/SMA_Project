using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Cubi
    {
        public float _x;
        public float _z;
        private string _nom;
        int _couleur;
        
        Light directionalLight;
        public Entity entcube;
        SceneNode ncube;

        public SceneNode Ncube
        {
            get { return ncube; }
            set { ncube = value; }
        }
        SceneNode nLum;

        public Cubi(SceneManager SceneManager, string nom, float x, float z, Random rand) 
        {

            ColourValue col = new ColourValue(1,0,0);
            entcube = SceneManager.CreateEntity("cube" + nom, "cube.mesh");
            _couleur = rand.Next(1, 6);
            MaterialPtr mat = MaterialManager.Singleton.Create(nom+"CubeMat", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
            TextureUnitState tuisTexture = mat.GetTechnique(0).GetPass(0).CreateTextureUnitState(color(_couleur));
            //entcube.SetMaterial(color(_couleur));
            //MaterialPtr myMat = new MaterialPtr( MaterialManager.Singleton.);
            //myMat.GetTechnique(0).GetPass(0).Diffuse=col;
            entcube.SetMaterialName(nom + "CubeMat");
            
            ncube = SceneManager.RootSceneNode.CreateChildSceneNode(nom + "nCube", new Vector3(x, 0.0f, z));
            
            ncube.Scale(0.5f, 0.01f, 0.5f);

            //nLum = ncube.CreateChildSceneNode(nom + "nLum", new Vector3(0.0f, 150.0f, 0.0f));

            

            /*directionalLight= SceneManager.CreateLight(nom+"directionalLight");
            directionalLight.Type = Light.LightTypes.LT_DIRECTIONAL;
            directionalLight.DiffuseColour = color(_couleur);
            directionalLight.SpecularColour = color(_couleur);
            directionalLight.Direction = new Vector3(0, -1, 0);*/

            ncube.AttachObject(entcube);
            //nLum.AttachObject(directionalLight);     

        }

        String color(int col) 
        {
            if (col == 1)
            {
                return "red.png";
            }
            else if (col == 2)
            {
                return "blue.png";
            }
            else if (col == 3)
            {
                return "green.png";
            }
            else if (col == 4)
            {
                return "pink.png";
            }
            else if (col == 5)
            {
                return "orange.png";
            }
            else 
            {
                return "Dirt.jpg";
            }
            
        }
 

    }
}
