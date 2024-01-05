using Networking.Implemetations.Connections;
using Networking.Implemetations.Packets.C2L;

namespace GameClient
{
    public partial class Form1 : Form
    {
        private Connection Connection { get; set; }
        public Form1()
        {
            InitializeComponent();
            this.Connection = new Connection("127.0.0.1", 23000, this);
        }

        private void OnLoginBtnClick(object sender, EventArgs e)
        {
            string Username = this.UserNameTxb.Text;
            if (string.IsNullOrEmpty(Username))
            {
                MessageBox.Show("Username cannot be empty!");
                return;
            }
            LoginPacket LoginPacket = new LoginPacket(Username);
            Connection.PushPacketToSendBuffer(LoginPacket);
        }
    }
}