using System;

public class TQDC2File
{
	public Open_File(String Path2File, OpenFileDialog TQDC_DATA)
	{
		TQDC_DATA.Filter = "Decoded files (*.txt)|*.txt|Raw files (*.dat)|*.dat|All files (*.*)|*.*";
		while (true)
		{
			DialogResult ResultDil;
			ResultDil = TQDC_DATA.ShowDialog();
			if (ResultDil == DialogResult.Cancel)
			{
				break;
			}
			if (ResultDil == DialogResult.OK)
			{
				DialogResult ResultMes;
				bool flag = false;
				Path2File = TQDC_DATA.FileName;
				var FSD = new FileStream(String.Format("{0}", TQDC_DATA.FileName), FileMode.Open);
				byte[] bbyte = new byte[4];
				byte[] fbyte = new byte[4]{ 0x50, 0x2a, 0x50, 0x22 };
				FSD.Read(bbyte, 0, 4);
				for (int i = 0; i < 4; i++)
				{
					if (bbyte[i] == fbyte[i])
					{
						flag = true;
					}
				}
				FSD.Close();
				if (!flag)
				{
					break;
				}
				ResultMes = MessageBox.Show("Выберете другой файл", "Неправильный файл", MessageBoxButtons.RetryCancel);
				if (ResultMes == DialogResult.Retry)
				{
					continue;
				}
				if (ResultMes == DialogResult.Cancel)
				{
					break;
				}
			}
		}
	}
}
