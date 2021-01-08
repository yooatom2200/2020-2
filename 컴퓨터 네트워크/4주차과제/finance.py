from pandas_datareader import data
import datetime

now = datetime.datetime.today()
today = now.strftime('%Y-%m-%d')
before = (now - datetime.timedelta(days=5)).strftime('%Y-%m-%d')
google_data = data.DataReader('005930.KS','yahoo', before, today)
print(google_data)
