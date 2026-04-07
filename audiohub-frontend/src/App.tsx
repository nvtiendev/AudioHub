import React, { useState } from 'react'
import api from './api'
import { Search, Download, Music, AlertCircle, CheckCircle2 } from 'lucide-react'
import { motion, AnimatePresence } from 'framer-motion'

const API_BASE = import.meta.env.VITE_API_BASE_URL || ''

interface Song {
  id: string
  title: string
  allArtistsNames: string
  bigThumbnailUrl: string
  duration: number
  streamingStatus: number
  isVIP: boolean
  fullUrl: string
}

interface Album {
  id: string
  title: string
  thumbnailUrl: string
  songList?: {
    items: Song[]
  }
}

function App() {
  const [query, setQuery] = useState('')
  const [song, setSong] = useState<Song | null>(null)
  const [album, setAlbum] = useState<Album | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')

  const handleSearch = async () => {
    if (!query) return
    setLoading(true)
    setError('')
    setSong(null)
    setAlbum(null)
    try {
      if (query.includes('/album/') || query.includes('/playlist/')) {
        const response = await api.get(`/api/audio/album/${encodeURIComponent(query)}`)
        setAlbum(response.data)
      } else {
        const response = await api.get(`/api/audio/song/${encodeURIComponent(query)}`)
        setSong(response.data)
      }
    } catch (err: any) {
      setError(err.response?.data?.message || 'Không tìm thấy nội dung. Vui lòng kiểm tra lại URL hoặc ID.')
    } finally {
      setLoading(false)
    }
  }

  const handleDownload = (id: string) => {
    window.open(`${API_BASE}/api/download/${id}`, '_blank')
  }

  const handleDownloadAlbum = (id: string) => {
    window.open(`${API_BASE}/api/download/album/${id}`, '_blank')
  }

  return (
    <div className="min-vh-100 flex flex-col items-center justify-center py-12 px-4">
      <div className="w-full max-w-2xl glass-card">
        <header className="mb-8 text-center">
          <h1 className="text-4xl font-bold bg-clip-text text-transparent bg-gradient-to-r from-indigo-400 to-purple-400 mb-2">
            AudioHub
          </h1>
          <p className="text-gray-400">Tải nhạc chất lượng cao & Album (.zip) chuyên nghiệp</p>
        </header>

        <div className="relative mb-8">
          <input
            type="text"
            className="input-field pr-12"
            placeholder="Dán link bài hát, album hoặc playlist..."
            value={query}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setQuery(e.target.value)}
            onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>) => e.key === 'Enter' && handleSearch()}
          />
          <button
            onClick={handleSearch}
            className="absolute right-2 top-1/2 -translate-y-1/2 p-2 text-indigo-400 hover:text-indigo-300 transition-colors"
          >
            <Search size={20} />
          </button>
        </div>

        <AnimatePresence mode="wait">
          {loading && (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
              className="flex justify-center py-12"
            >
              <div className="w-8 h-8 border-4 border-indigo-500 border-t-transparent rounded-full animate-spin"></div>
            </motion.div>
          )}

          {error && (
            <motion.div
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              className="flex items-center gap-3 p-4 bg-red-500/10 border border-red-500/20 rounded-xl text-red-400 mb-6"
            >
              <AlertCircle size={20} />
              <span>{error}</span>
            </motion.div>
          )}

          {song && !loading && (
            <motion.div
              initial={{ opacity: 0, scale: 0.95 }}
              animate={{ opacity: 1, scale: 1 }}
              className="p-4 rounded-2xl bg-white/5 border border-white/10 flex flex-col md:flex-row gap-6 items-center song-card"
            >
              <div className="relative group">
                <img
                  src={song.bigThumbnailUrl}
                  alt={song.title}
                  className="w-32 h-32 rounded-xl object-cover shadow-2xl"
                />
                <div className="absolute inset-0 bg-black/40 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity rounded-xl">
                  <Music className="text-white" size={24} />
                </div>
              </div>

              <div className="flex-1 text-center md:text-left">
                <div className="flex items-center justify-center md:justify-start gap-2 mb-1">
                  <h2 className="text-xl font-bold">{song.title}</h2>
                  {song.isVIP && (
                    <span className="px-2 py-0.5 bg-yellow-500/20 text-yellow-500 text-xs font-bold rounded border border-yellow-500/30">
                      VIP
                    </span>
                  )}
                </div>
                <p className="text-indigo-400 mb-4">{song.allArtistsNames}</p>
                
                <div className="flex flex-wrap gap-3 justify-center md:justify-start">
                  <button
                    onClick={() => handleDownload(song.id)}
                    disabled={song.isVIP}
                    className={`btn-primary ${song.isVIP ? 'opacity-50 cursor-not-allowed grayscale' : ''}`}
                  >
                    <Download size={18} />
                    {song.isVIP ? 'Chỉ dành cho VIP' : 'Tải nhạc (.mp3)'}
                  </button>
                  <a
                    href={song.fullUrl}
                    target="_blank"
                    rel="noreferrer"
                    className="px-4 py-2 rounded-xl border border-white/10 hover:bg-white/5 transition-colors text-sm flex items-center gap-2"
                  >
                    Nghe tại nguồn
                  </a>
                </div>
              </div>
            </motion.div>
          )}

          {album && !loading && (
            <motion.div
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              className="flex flex-col gap-6"
            >
              <div className="p-4 rounded-2xl bg-white/5 border border-white/10 flex flex-col md:flex-row gap-6 items-center">
                <img src={album.thumbnailUrl} alt={album.title} className="w-32 h-32 rounded-xl object-cover" />
                <div className="flex-1 text-center md:text-left">
                  <h2 className="text-2xl font-bold mb-1">{album.title}</h2>
                  <p className="text-indigo-400 mb-4">{album.songList?.items.length || 0} bài hát</p>
                  <button onClick={() => handleDownloadAlbum(album.id)} className="btn-primary">
                    <Download size={18} />
                    Tải Album (.zip)
                  </button>
                </div>
              </div>

              <div className="flex flex-col gap-2">
                {album.songList?.items.map((s) => (
                  <div key={s.id} className="flex items-center gap-6 p-3 rounded-xl hover:bg-white/5 transition-all group">
                    <img
                      src={s.bigThumbnailUrl}
                      alt={s.title}
                      className="w-12 h-12 rounded-2xl object-cover shadow-lg border border-white/10"
                    />
                    <div className="flex-1 min-w-0">
                      <div className="flex items-center gap-2">
                        <p className="font-medium truncate">{s.title}</p>
                        {s.isVIP && <span className="text-[10px] px-1.5 py-0.5 bg-yellow-500/20 text-yellow-500 rounded border border-yellow-500/30 font-bold">VIP</span>}
                      </div>
                      <p className="text-xs text-gray-400 truncate">{s.allArtistsNames}</p>
                    </div>
                    {!s.isVIP && (
                      <button 
                        onClick={() => handleDownload(s.id)} 
                        className="btn-download-small"
                        title="Tải về máy"
                      >
                        <Download size={18} />
                      </button>
                    )}
                  </div>
                ))}
              </div>
            </motion.div>
          )}
        </AnimatePresence>

        <footer className="mt-12 pt-8 border-t border-white/5 text-center text-gray-500 text-sm">
          <div className="flex justify-center gap-6">
            <div className="flex items-center gap-1">
              <CheckCircle2 size={14} className="text-green-500" />
              <span>CORS Enabled</span>
            </div>
            <div className="flex items-center gap-1">
              <CheckCircle2 size={14} className="text-green-500" />
              <span>Proxy Download</span>
            </div>
          </div>
          <p className="mt-4">Build with ❤️ for Audio Enthusiasts</p>
        </footer>
      </div>
    </div>
  )
}

export default App
